using System.ComponentModel.DataAnnotations;

public class AnticipationModel
{
    [Required(ErrorMessage = "CardTransactionId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "CardTransactionId is required.")]
    public int CardTransactionId { get; set; }

    [Range(1, 9999999999999999.99, ErrorMessage = "RequestAmount must be valid.")]
    public decimal RequestAmount { get; set; }
    public Anticipation MapTo()
    {
        return new Anticipation()
        {
            CardTransactionId = this.CardTransactionId,
            RequestAmount = this.RequestAmount
        };
    }
}