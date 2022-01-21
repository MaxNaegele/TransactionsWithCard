using Microsoft.EntityFrameworkCore;

public static class AnticipationQuery
{
    public static IQueryable<Anticipation> WhereId(this IQueryable<Anticipation> anticipations, int id)
    {
        return anticipations.Where(x => x.Id.Equals(id));
    }
    public static IQueryable<Anticipation> WhereStatus(this IQueryable<Anticipation> anticipations, StatusAnticipation status)
    {
        return anticipations.Where(x => x.StatusAnticipation.Equals(status));
    }

       public static IQueryable<Anticipation> IncludeTransaction(this IQueryable<Anticipation> anticipations)
    {
        return anticipations.Include(x => x.CardTransaction);
    }
}