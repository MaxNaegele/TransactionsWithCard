using System.ComponentModel.DataAnnotations;

public class CreateCardTransactionModel
{
    public int Nsu { get; set; }
    [Required(ErrorMessage = "CardNumver is required.")]
    [StringLengthRangeAttribute(Minimum = 16, ErrorMessage = "Must contain > 16 characters.")]
    public string CardNumber { get; set; }

    [Range(1, 9999999999999999.99, ErrorMessage = "GrossTransactionValue must be valid.")]
    public decimal GrossTransactionValue { get; set; }

    [Required(ErrorMessage = "NumberParcel is required")]
    public int NumberParcel { get; set; }

        public CardTransaction MapTo()
        {
            return new CardTransaction()
            {
                Nsu = this.Nsu,
                TransactionDate = DateTime.Now,
                CardNumber = this.CardNumber,
                GrossTransactionValue = this.GrossTransactionValue,
                NumberParcel = NumberParcel
            };
        }

}