using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMemoryCache _memoryCache;
        public TicketService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public Task CreateTicketAsync(Ticket ticket)
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);
            if (tickets == null)
            {
                var newTickets = new List<Ticket>();
                newTickets.Add(ticket);
                _memoryCache.Set(Constant.Ticket, newTickets);
            }
            else
            {
                tickets.Add(ticket);
                _memoryCache.Set(Constant.Ticket, tickets);
            }

            return Task.CompletedTask;
        }
        public Task<List<Ticket>> GetTicketsAsync()
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);
            return Task.FromResult(tickets);
        }

        public Task<Ticket?> GetTicketAsync(Guid id)
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);
            if (tickets != null)
            {
                var ticket = tickets.FirstOrDefault(item => item.Id == id);
                return Task.FromResult(ticket);
            }
            return Task.FromResult(default(Ticket));
        }

        public Task DeleteTicketAsync(Guid id)
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);

            if (tickets != null)
            {
                var ticket = tickets.FirstOrDefault(item => item.Id == id);
                if (ticket != null)
                {
                    tickets.Remove(ticket);
                }
            }

            return Task.CompletedTask;
        }

        public Task UpdateTicketAsync(Guid id, string title, string summary, string description, string account)
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);
            if (tickets != null)
            {
                var ticket = tickets.FirstOrDefault(item => item.Id == id);
                if (ticket != null)
                {
                    ticket.Title = ticket.Title == title ? ticket.Title : title;
                    ticket.Summary = ticket.Summary == summary ? ticket.Summary : summary;
                    ticket.Description = ticket.Description == description ? ticket.Description : description;
                    ticket.UpdateBy = ticket.UpdateBy == account ? ticket.UpdateBy : account;
                    ticket.UpdateDate = DateTimeOffset.UtcNow;
                }
            }
            return Task.CompletedTask;
        }
        
        public Task UpdateTicketStatus(Guid id, TicketStatus ticketStatus, string account)
        {
            var tickets = _memoryCache.Get<List<Ticket>>(Constant.Ticket);
            if (tickets != null)
            {
                var ticket = tickets.FirstOrDefault(item => item.Id == id);
                if (ticket != null)
                {
                    ticket.TicketStatus = ticket.TicketStatus == ticketStatus ? ticket.TicketStatus : ticketStatus;
                    ticket.UpdateBy = account;
                    ticket.UpdateDate = DateTimeOffset.UtcNow;
                }
            }
            return Task.CompletedTask;
        }

        public Task<List<TicketStatus>> GetTicketStatusAsync(RoleType roleType)
        {
            var mapping = _memoryCache.Get<Dictionary<RoleType, List<TicketStatus>>>(Constant.TickStatusMapping);
            if (mapping != null)
            {
                var status = mapping.ContainsKey(roleType) ? mapping[roleType] : new List<TicketStatus>();
                return Task.FromResult(status);
            }

            return Task.FromResult(default(List<TicketStatus>));
        }
    }
}
