using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OptionController : BaseController
    {
        private readonly ITicketService _ticketService;

        public OptionController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("")]
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> GetStatusAsync()
        {
            var status = await _ticketService.GetTicketStatusAsync(base.Role);
            var result = status.Select(item => new
            {
                Name = item.ToString(),
                Value = item
            });
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, result));

        }
    }
}
