using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

namespace Transactions.Api.Models.Services
{
    public class AnticipationService : IAnticipationService
    {
        private readonly AppDbContext _context;

        private IParcelService _parcelService;
        private ICardTransactionService _cardTransactionService;
        public AnticipationService(AppDbContext context, IParcelService parcelService, ICardTransactionService cardTransactionService)
        {
            _context = context;
            _parcelService = parcelService;
            _cardTransactionService = cardTransactionService;
        }

        public async Task<List<CardTransaction>> ConsultTransactions()
        {
            return await _context.CardTransactions.IncludeAnticipationPending().ToListAsync();
        }

        public async Task<bool> CreateAnticipation(Anticipation entity)
        {
            entity.RequestDate = DateTime.Now;
            entity.StatusAnticipation = StatusAnticipation.pending;
            await _context.Anticipations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> StartAnticipationService(int idAnticipation)
        {

            var entity = await _context.Anticipations.WhereId(idAnticipation).SingleAsync();

            entity.StatusAnticipation = StatusAnticipation.underAnalysis;
            entity.AnalysisDate = DateTime.Now;

            try
            {
                _context.Anticipations.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public async Task<bool> ApproveAnticipation(int idAnticipation)
        {
            var entity = await _context.Anticipations.WhereId(idAnticipation).SingleAsync();
            entity.ResultAnalysis = ResultAnalysis.Approved;
            entity.AnalysisCompletionDate = DateTime.Now;
            entity.StatusAnticipation = StatusAnticipation.finalized;
            entity.AnticipatedValue = await _parcelService.SetValueAnticipation(entity.CardTransactionId, entity.RequestAmount);

            _context.Anticipations.Update(entity);
            await _context.SaveChangesAsync();

            await _cardTransactionService.ConfirmAnticipationTransaction(entity.CardTransactionId);

            return true;
        }
        public async Task<bool> DisapproveAnticipation(int idAnticipation)
        {
            var entity = await _context.Anticipations.WhereId(idAnticipation).SingleAsync();
            entity.ResultAnalysis = ResultAnalysis.Disapproved;
            entity.AnalysisCompletionDate = DateTime.Now;
            entity.StatusAnticipation = StatusAnticipation.pending;

            _context.Anticipations.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Anticipation>> ConsultAnticipation(StatusAnticipation status)
        {
            return await _context.Anticipations.WhereStatus(status).IncludeTransaction().ToListAsync();
        }
    }
}