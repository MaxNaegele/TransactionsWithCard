using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transactions.Api.Models.Services.Interfaces;

[Route("api/v1/anticipation")]
public sealed class AnticipationController : Controller
{

    private readonly AppDbContext _context;
    private IAnticipationService _anticipationservice;
    public AnticipationController(AppDbContext context, IAnticipationService anticipationservice)
    {
        _context = context;
        _anticipationservice = anticipationservice;
    }

    [HttpGet, Route("disposable-anticipation")]
    public async Task<IActionResult> DisposableAnticipation()
    {
        return Ok(await _anticipationservice.ConsultTransactions());
    }

    [HttpPost, Route("create-anticipation")]
    public async Task<IActionResult> CreateAnticipation([FromBody] AnticipationModel model)
    {
        var entity = model.MapTo();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.Select(x => x.Errors));
        }

        var cardTransaction = await _context.CardTransactions.WhereId(entity.CardTransactionId).SingleOrDefaultAsync();
        if (cardTransaction == null) return NotFound("Transaction not found.");

        if (model.RequestAmount <= cardTransaction.LiquidTransactionValue)
        {
            if (await _anticipationservice.CreateAnticipation(entity)) return NoContent();
        }
        else
        {
            return BadRequest($"Requested amount greater than {cardTransaction.LiquidTransactionValue} balance allowed.");
        }

        return BadRequest();
    }

    [HttpPost, Route("start-anticipation-service")]
    public async Task<IActionResult> StartAnticipationService([FromQuery] int idAnticipation)
    {
        var entity = await _context.Anticipations.WhereId(idAnticipation).SingleOrDefaultAsync();
        if (entity == null) return NotFound("Anticipation not found.");

        if (!await _anticipationservice.StartAnticipationService(idAnticipation)) return BadRequest();

        return NoContent();
    }

    [HttpPost, Route("approve_anticipation")]
    public async Task<IActionResult> ApproveAnticipation([FromBody] List<int> listIdAnticipation)
    {
        foreach (int item in listIdAnticipation)
        {
            var entity = await _context.Anticipations.WhereId(item).SingleOrDefaultAsync();
            if (entity == null) return NotFound("Anticipation not found.");
            if (entity.ResultAnalysis == ResultAnalysis.Disapproved) return BadRequest($"Anticipation {item} disapproved.");

            await _anticipationservice.ApproveAnticipation(item);
        }
        return NoContent();
    }

    [HttpPost, Route("disapprove_anticipation")]
    public async Task<IActionResult> DisapproveAnticipation([FromBody] List<int> listIdAnticipation)
    {
        foreach (int item in listIdAnticipation)
        {
            var entity = await _context.Anticipations.WhereId(item).SingleOrDefaultAsync();
            if (entity == null) return NotFound($"Anticipation {item} not found.");
            if (entity.ResultAnalysis == ResultAnalysis.PartialApproved) return BadRequest($"Anticipation {item} partital approved.");

            await _anticipationservice.DisapproveAnticipation(item);
        }

        return NoContent();
    }


    [HttpGet, Route("consult-anticipation")]
    public async Task<IActionResult> ConsultAnticipation([FromQuery] StatusAnticipation status)
    {
        return Ok(await _anticipationservice.ConsultAnticipation(status));
    }
}
