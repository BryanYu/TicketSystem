﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "QA,PM")]
        public async Task<ActionResult> CreateTicketAsync([FromBody] CreateTicketRequest request)
        {
            var allowTickeType = await _ticketService.GetTicketTypeAsync(base.Role);
            if (allowTickeType.All(item => item != request.TicketType))
            {
                return BadRequest(new BaseResponse<object>(ApiResponseCode.TicketTypeNotAllow, null));
            }


            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketType = request.TicketType,
                Description = request.Description,
                Summary = request.Summary,
                Title = request.Title,
                Severity = request.Severity,
                Priority = request.Priority,
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
        [Authorize(Roles = "RD,QA,PM")]
        public async Task<ActionResult> UpdateTicketAsync([FromRoute] Guid id, [FromBody] UpdateTicketRequest request)
        {
            var allowStatus = await _ticketService.GetTicketStatusAsync(base.Role);
            if (allowStatus.All(item => item != request.TicketStatus))
            {
                return BadRequest(new BaseResponse<object>(ApiResponseCode.StatusNotAllow, null));
            }

            await _ticketService.UpdateTicketAsync(id, request.Title, request.Summary, request.Description,
                request.TicketStatus, request.Severity, request.Priority, base.Account);
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
        [Authorize(Roles = "RD,QA,PM")]
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
                TicketStatus = ticket.TicketStatus,
                Severity = ticket.Severity,
                Priority = ticket.Priority
            };
            return Ok(new BaseResponse<GetTicketResponse>(ApiResponseCode.Success, response));
        }

        [HttpGet]
        [Authorize(Roles = "RD,QA,PM")]
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
                TicketType = item.TicketType,
                TicketTypeName = Enum.GetName(item.TicketType),
                TicketStatus = item.TicketStatus,
                TicketStatusName = Enum.GetName(item.TicketStatus),
                Summary = item.Summary,
                Description = item.Description,
                Severity = item.Severity,
                Priority = item.Priority,
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
            await _ticketService.ResolveTicketAsync(id, base.Account);
            return Ok(new BaseResponse<object>(ApiResponseCode.Success, null));
        }
    }
}
