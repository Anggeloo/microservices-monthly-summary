using microservices_monthly_summary.Models;
using microservices_monthly_summary.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class MonthlySummaryController : Controller
{
    private readonly MonthlySummaryService _service;

    public MonthlySummaryController(MonthlySummaryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ApiResponse<List<MonthlySummary>>("success", result, "List of monthly summary"));
    }

    [HttpGet("{codice}")]
    public async Task<IActionResult> GetOrderByCode(string codice)
    {

        var result = await _service.GetByCodeAsync(codice);

        if (result == null)
        {
            return Ok(new ApiResponse<MonthlySummary>("empty", result, "Monthly summary not found"));
        }

        return Ok(new ApiResponse<MonthlySummary>("success", result, "Monthly summary found"));
    }

    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] MonthlySummary model)
    {
        if (model == null)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "Monthly summary graphic data"));
        }


        model.SummaryCode = await _service.GenerateNextOrderCodeAsync();

        var created = await _service.CreateAsync(model);

        if (created == null)
        {
            return StatusCode(500, new ApiResponse<string>("Error", null, "Monthly summary was created but could not be retrieved"));
        }

        return CreatedAtAction(nameof(Create),
            new { codice = created.SummaryCode },
            new ApiResponse<MonthlySummary>("success", created, "Information graphic created successfully"));
    }

    [HttpPut("update/{codice}")]
    public async Task<IActionResult> CreateOrder(string codice, [FromBody] MonthlySummary model)
    {
        if (model == null)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "Invalid Information monthly summary data"));
        }

        var exits = await _service.CheckIfExistsAsync(codice);

        if (exits == false)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "The monthly summary code does not exist"));
        }

        var updated = await _service.UpdateAsync(codice, model);

        if (updated == null)
        {
            return StatusCode(400, new ApiResponse<string>("Error", null, "Monthly summary was updated but could not be retrieved"));
        }

        return Ok(new ApiResponse<MonthlySummary>("success", updated, "Monthly summary updated successfully"));
    }

    [HttpDelete("delete/{codice}")]
    public async Task<IActionResult> DeñeteOrder(string codice)
    {
        var exitsOrder = await _service.CheckIfExistsAsync(codice);

        if (exitsOrder == false)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "The monthly summary code does not exist"));
        }

        var delete = await _service.DeleteAsync(codice);

        if (delete == null)
        {
            return StatusCode(400, new ApiResponse<string>("Error", null, "Error ..."));
        }

        return Ok(new ApiResponse<MonthlySummary>("success", delete, "Monthly summary deleted successfully"));
    }
}

