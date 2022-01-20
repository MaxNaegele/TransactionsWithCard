using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

namespace Transactions.Api.Models.Services
{
    public class CardTransactionService : ICardTransactionService
    {
        private readonly AppDbContext _context;
        public CardTransactionService(AppDbContext context)
        {
            _context = context;
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

                var parcels = new List<Parcel>();
                for (var i = 0; i < entity.NumberParcel; i++)
                {
                    int numberParcel = (i + 1);
                    parcels.Add(new Parcel()
                    {
                        GrossValue = entity.GrossTransactionValue / entity.NumberParcel,
                        LiquidValue = entity.LiquidTransactionValue / entity.NumberParcel,
                        NumberParcel = numberParcel,
                        ExpectedReceiptDate = entity.TransactionDate.AddDays(numberParcel * 30)
                    });
                }
                entity.Parcels = parcels;
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