namespace Transactions.Api.Models.Services.Interfaces
{
    public interface IParcelService
    {
        List<Parcel> GenerateParcels(CardTransaction cardTransaction);
        Task<decimal> SetValueAnticipation(int idTransaction, decimal anticipationValue);
    }
}