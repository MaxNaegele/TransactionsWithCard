using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

namespace Transactions.Api.Models.Services
{
    public class ParcelService : IParcelService
    {
        private readonly AppDbContext _context;
        public ParcelService(AppDbContext context)
        {
            _context = context;
        }

        public List<Parcel> GenerateParcels(CardTransaction cardTransaction)
        {
            var parcels = new List<Parcel>();
            for (var i = 0; i < cardTransaction.NumberParcel; i++)
            {
                int numberParcel = (i + 1);
                parcels.Add(new Parcel()
                {
                    GrossValue = cardTransaction.GrossTransactionValue / cardTransaction.NumberParcel,
                    LiquidValue = cardTransaction.LiquidTransactionValue / cardTransaction.NumberParcel,
                    NumberParcel = numberParcel,
                    ExpectedReceiptDate = cardTransaction.TransactionDate.AddDays(numberParcel * 30)
                });
            }
            return parcels;
        }

        public async Task<decimal> SetValueAnticipation(int idTransaction, decimal anticipationValue)
        {
            decimal valueTotal = 0, tax = (decimal)0.038;
            var transaction = await _context.CardTransactions.WhereId(idTransaction).IncludeParcel().SingleAsync();
            foreach (Parcel item in transaction.Parcels)
            {
                if (anticipationValue >= item.LiquidValue)
                {
                    decimal valeu = (item.LiquidValue / anticipationValue) * anticipationValue;
                    item.ValueAnticipation = valeu - (tax * valeu);
                    anticipationValue = anticipationValue - valeu;
                }
                else
                {
                    item.ValueAnticipation = anticipationValue - (tax * anticipationValue);
                    anticipationValue = 0;
                }
                valueTotal = valueTotal + item.ValueAnticipation.Value;
                item.ExpectedReceiptDate = DateTime.Now;
                _context.SaveChanges();
                if (anticipationValue == 0) break;
            }
            return valueTotal;
        }
    }
}