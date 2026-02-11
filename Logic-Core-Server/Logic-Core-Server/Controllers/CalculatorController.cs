using Logic_Core_Server.Core.DTOs;
using Logic_Core_Server.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Logic_Core_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculationService _calcService;
        public CalculatorController(ICalculationService calcService) => _calcService = calcService;

        [HttpGet("operations")]
        public async Task<IActionResult> GetOperations() => Ok(await _calcService.GetAvailableOperationsAsync());

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest req)
        {
            // ה-Service עכשיו מחזיר CalculationResponse הכולל הכל
            var response = await _calcService.CalculateAsync(req.Action, req.FieldA, req.FieldB);
            return Ok(response);
        }
    }
}
