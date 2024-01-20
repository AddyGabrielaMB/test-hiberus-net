using Microsoft.AspNetCore.Mvc;
using TestHiberusNet.AppServices.TerminalServices;
using TestHiberusNet.DTOs.TerminalDTOs;
using TestHiberusNet.Models;

namespace TestHiberusNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalsController : ControllerBase
    {
        private readonly ITerminalService _terminalService;
        public TerminalsController(ITerminalService terminalService) {
            this._terminalService = terminalService;
        }

        [HttpGet("FullInfo")]
        public async Task<IActionResult> GetTerminals()
        {
            List<TerminalDto> terminals;
            try
            {
                terminals = await _terminalService.GetList();
                if (terminals.Count == 0) return  NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(terminals);
        }
    }
}
