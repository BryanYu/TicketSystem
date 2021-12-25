using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.API.ActionFilters;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TokenAuthorization]
    public class OptionController : BaseController
    {
        private readonly ITicketService _ticketService;

        public OptionController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("TicketStatus")]
        [Authorize(Roles = "RD,QA,PM")]
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

        [HttpGet("TicketType")]
        [Authorize(Roles = "QA,PM")]
        public async Task<ActionResult> GetTicketTypeAsync()
        {
            var ticketTypes = await _ticketService.GetTicketTypeAsync(base.Role);
            var result = ticketTypes.Select(item => new
            {
                Name = item.ToString(),
                Value = item
            });
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, result));
        }
    }
}
