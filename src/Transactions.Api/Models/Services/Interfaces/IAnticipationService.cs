namespace Transactions.Api.Models.Services.Interfaces
{
    public interface IAnticipationService
    {
        Task<List<CardTransaction>> ConsultTransactions();
        Task<bool> CreateAnticipation(Anticipation entity); 

        Task<bool> StartAnticipationService(int idAnticipation);

        Task<bool> ApproveAnticipation(int idAnticipation);
        Task<bool> DisapproveAnticipation(int idAnticipation);
        Task<List<Anticipation>> ConsultAnticipation(StatusAnticipation status);
    }
}