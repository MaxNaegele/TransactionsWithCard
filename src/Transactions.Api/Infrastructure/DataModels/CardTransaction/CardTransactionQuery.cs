using Microsoft.EntityFrameworkCore;

public static class CardTransactionQuery
{
    public static IQueryable<CardTransaction> WhereId(this IQueryable<CardTransaction> cardtransactions, int id)
    {
        return cardtransactions.Where(x => x.Id.Equals(id));
    }
    public static IQueryable<CardTransaction> WhereOnlyAnticipation(this IQueryable<CardTransaction> cardtransactions)
    {
        return cardtransactions.Where(x => x.Anticipad.Equals(false));
    }
    public static IQueryable<CardTransaction> IncludeParcel(this IQueryable<CardTransaction> cardtransactions)
    {
        return cardtransactions.Include(x => x.Parcels);
    }
    public static IQueryable<CardTransaction> IncludeAnticipation(this IQueryable<CardTransaction> cardtransactions)
    {
        return cardtransactions.Include(x => x.Anticipations);
    }
    public static IQueryable<CardTransaction> IncludeAnticipationPending(this IQueryable<CardTransaction> cardtransactions)
    {
        return cardtransactions.Include(x => x.Anticipations.Where(y => y.StatusAnticipation != StatusAnticipation.finalized));
    }
}