namespace Transactions.Api.Models.Services.Interfaces
{
    public interface ICardTransactionService
    {
        Task<IEnumerable<CardTransaction>> ListTransaction(int nsu);
        Task<bool> CreatePayWithCard(CardTransaction model);
    }
}