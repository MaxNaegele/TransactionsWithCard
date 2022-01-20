public sealed class CardTransaction
{
    public int Id { get; set; }
    public int Nsu { get; set; }    
    public string CardNumber { get; set; }    
    public DateTime TransactionDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public DateTime? DisapprovalDate { get; set; }
    public bool Anticipad { get; set; }
    public AcquirerConfirmation AcquirerConfirmation { get; set; }
    public decimal GrossTransactionValue { get; set; }
    public decimal LiquidTransactionValue { get; set; }
    public decimal FixedFeeCharged { get; set; }
    public int NumberParcel { get; set; }
    public string LastFourDigitsCard { get; set; }
    public ICollection<Parcel> Parcels { get; set; }
}
