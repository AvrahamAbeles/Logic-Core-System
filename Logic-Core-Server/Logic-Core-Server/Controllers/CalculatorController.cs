namespace Logic_Core_Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CalculatorController(ICalculationService calcService) : ControllerBase
{
    [HttpGet("operations")]
    public async Task<IActionResult> GetOperations()
    {
        return Ok(await calcService.GetAvailableOperationsAsync());
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest req)
    {
        var response = await calcService.CalculateAsync(req.Action, req.FieldA, req.FieldB);
        return Ok(response);
    }
}

