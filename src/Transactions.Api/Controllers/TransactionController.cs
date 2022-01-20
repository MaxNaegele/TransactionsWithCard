using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

[Route("api/v1/transaction")]
public sealed class TransactionController : Controller
{

    private readonly AppDbContext _context;

    private ICardTransactionService _cardtransactionservice;
    public TransactionController(AppDbContext context, ICardTransactionService cardtransactionservice)
    {
        _context = context;
        _cardtransactionservice = cardtransactionservice;
    }

    [HttpPost, Route("pay-with-card")]
    public async Task<IActionResult> PayWithCard([FromBody] CreateCardTransactionModel model)
    {
        var entity = model.MapTo();
        if (await _cardtransactionservice.CreatePayWithCard(entity))
            return NoContent();

        return BadRequest();
    }


    [HttpGet, Route("transaction-list/{nsu:int}")]
    public async Task<IActionResult> TransactionList([FromRoute] int nsu)
    {    
        return Ok(await _cardtransactionservice.ListTransaction(nsu));
    }
}
