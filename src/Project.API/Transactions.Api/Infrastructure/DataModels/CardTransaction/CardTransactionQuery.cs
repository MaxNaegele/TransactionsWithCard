using Microsoft.EntityFrameworkCore;

public static class CardTransactionQuery
{
    public static IQueryable<CardTransaction> WhereId(this IQueryable<CardTransaction> cardtransactions, int id)
    {
        return cardtransactions.Where(x => x.Id.Equals(id));
    }
    public static IQueryable<CardTransaction> IncludeParcel(this IQueryable<CardTransaction> cardtransactions)
    {
        return cardtransactions.Include(x => x.Parcels);
    }
}