public sealed class Anticipation
{
    public int Id { get; set; }
    public int CardTransactionId { get; set; }
    public DateTime RequestDate { get; set; }

    public DateTime AnalysisDate { get; set; }

    public DateTime AnalysisCompletionDate { get; set; }

    public ResultAnalysis? ResultAnalysis { get; set; }

    public decimal RequestAmount { get; set; }

    public decimal? AnticipatedValue { get; set; }
    public StatusAnticipation StatusAnticipation { get; set; }
    
    public CardTransaction CardTransaction { get; set; }

}