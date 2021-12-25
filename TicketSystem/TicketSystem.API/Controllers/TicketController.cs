using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TicketSystem.API.ActionFilters;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;
using TicketSystem.Core.Models.Request;
using TicketSystem.Core.Models.Response;
using TicketSystem.Core.Services;

namespace TicketSystem.API.Controllers
{
    [Route("api/v1/[controller]")]
    [TokenAuthorization]
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
                TicketType = TicketType.Bug,
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
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> UpdateTicketAsync([FromRoute] Guid id, [FromBody] UpdateTicketRequest request)
        {
            var allowStatus = await _ticketService.GetTicketStatusAsync(base.Role);
            if (allowStatus.All(item => item != request.TicketStatus))
            {
                return BadRequest(new BaseResponse<object>(ApiResponseCode.StatusNotAllow, null));
            }

            await _ticketService.UpdateTicketAsync(id, request.Title, request.Summary, request.Description,
                request.TicketStatus, base.Account);
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
            if (ticket == null)
            {
                return BadRequest(new BaseResponse<object>(ApiResponseCode.DataNotFound, null));
            }
            var response = new GetTicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Summary = ticket.Summary,
                TicketStatus = ticket.TicketStatus.ToString()
            };
            return Ok(new BaseResponse<GetTicketResponse>(ApiResponseCode.Success, response));
        }

        [HttpGet]
        [Authorize(Roles = "RD,QA")]
        public async Task<ActionResult> GetTicketsAsync()
        {
            var tickets = await _ticketService.GetTicketsAsync();
            if (tickets == null)
            {
                return Ok(new BaseResponse<List<GetTicketsResponse>>(ApiResponseCode.Success, null));
            }
            var response = tickets.Select(item => new GetTicketsResponse
            {
                Id = item.Id,
                Title = item.Title,
                TicketType = item.TicketType.ToString(),
                TicketStatus = item.TicketStatus.ToString(),
                Summary = item.Summary,
                Description = item.Description,
                CreateBy = item.CreateBy,
                UpdateBy = item.UpdateBy,
                CreateDate = item.CreateDate.ToUnixTimeSeconds(),
                UpdateDate = item.CreateDate.ToUnixTimeSeconds()
            });

            return Ok(new BaseResponse<IEnumerable<GetTicketsResponse>>(ApiResponseCode.Success, response));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "RD")]
        public async Task<ActionResult> ResolveTicketAsync([FromRoute]Guid id)
        {
            await _ticketService.ResolveTicketAsync(id);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }
    }
}
