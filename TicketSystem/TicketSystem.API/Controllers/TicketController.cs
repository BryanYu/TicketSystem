using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Request;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Authorize(Roles = "QA")]
        public async Task<ActionResult> CreateTicketAsync([FromBody] CreateTicketRequest request)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketType = request.TicketType,
                Description = request.Description,
                Summary = request.Summary,
                Title = request.Title,
                CreateBy = base.Account,
                UpdateBy = base.Account,
                CreateDate = DateTimeOffset.UtcNow,
                UpdateDate = DateTimeOffset.UtcNow,
                TicketStatus = TicketStatus.New
            };
            await _ticketService.CreateTicketAsync(ticket);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "QA")]
        public async Task<ActionResult> UpdateTicketAsync([FromRoute] Guid id, [FromBody] UpdateTicketRequest request)
        {
            await _ticketService.UpdateTicketAsync(id, request.Title, request.Summary, request.Description,
                base.Account);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "QA")]
        public async Task<ActionResult> DeleteTicketAsync([FromRoute] Guid id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> GetTicket([FromRoute] Guid id)
        {
            var ticket = await _ticketService.GetTicketAsync(id);
            return Ok(new BaseResponse<Ticket?>(ApiResponseCode.Success, ticket));
        }

        [HttpGet]
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> GetTicketsAsync()
        {
            var result = await _ticketService.GetTicketsAsync();

            return Ok(new BaseResponse<List<Ticket>>(ApiResponseCode.Success, result));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> UpdateTicketStatusAsync([FromRoute] Guid id, [FromBody] UpdateTicketStatusRequest request)
        {
            var allowStatus = await _ticketService.GetTicketStatusAsync(base.Role);
            if (allowStatus.All(item => item != request.TicketStatus))
            {
                return BadRequest(new BaseResponse<object>(ApiResponseCode.StatusNotAllow, null));
            }
            
            await _ticketService.UpdateTicketStatus(id, request.TicketStatus, base.Account);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }
        

    }
}
