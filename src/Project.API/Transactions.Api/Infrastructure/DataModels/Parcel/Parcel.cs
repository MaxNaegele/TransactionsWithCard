    public sealed class Parcel
    {
        public int Id { get; set; }
        public int CardTransactionId { get; set; }
        public decimal GrossValue { get; set; }
        public decimal LiquidValue { get; set; }
        public int NumberParcel { get; set; }
        public decimal? ValueAnticipation { get; set; }        
        public DateTime ExpectedReceiptDate { get; set; }
        public DateTime? TransferredParcelDate { get; set; }                                    
        public CardTransaction CardTransaction { get; set; }
    }
