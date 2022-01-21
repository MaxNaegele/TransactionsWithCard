using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

namespace Transactions.Api.Models.Services
{
    public class CardTransactionService : ICardTransactionService
    {
        private readonly AppDbContext _context;
        private IParcelService _parcelService;
        public CardTransactionService(AppDbContext context, IParcelService parcelService)
        {
            _context = context;
            _parcelService = parcelService;
        }

        public async Task<bool> ConfirmAnticipationTransaction(int id)
        {
            var transaction = await _context.CardTransactions.WhereId(id).SingleAsync();
            transaction.Anticipad = true;
            _context.CardTransactions.Update(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreatePayWithCard(CardTransaction entity)
        {
            entity.FixedFeeCharged = (decimal)0.9;
            entity.LiquidTransactionValue = entity.GrossTransactionValue - entity.FixedFeeCharged;
            entity.LastFourDigitsCard = entity.CardNumber.Substring(entity.CardNumber.Length - 4, 4);

            if (entity.CardNumber.Substring(0, 4).Equals("5999")) entity.DisapprovalDate = DateTime.Now;
            else
            {
                entity.ApprovalDate = DateTime.Now;
                entity.AcquirerConfirmation = AcquirerConfirmation.Approved;
                entity.Parcels = _parcelService.GenerateParcels(entity);
            }

            _context.CardTransactions.Add(entity);
            _context.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<CardTransaction>> ListTransaction(int nsu)
        {
            return await _context.CardTransactions.WhereId(nsu).IncludeParcel().ToListAsync();
        }
    }
}