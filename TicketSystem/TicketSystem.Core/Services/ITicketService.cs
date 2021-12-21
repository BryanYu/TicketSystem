using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Services
{
    public interface ITicketService
    {
        public Task CreateTicketAsync(Ticket ticket);
        public Task<List<Ticket>> GetTicketsAsync();
        public Task<Ticket?> GetTicketAsync(Guid id);
        public Task DeleteTicketAsync(Guid id);
        public Task UpdateTicketAsync(Guid id, string title, string summary, string description, string account);
        public Task UpdateTicketStatus(Guid id, TicketStatus ticketStatus, string account);
        Task<List<TicketStatus>> GetTicketStatusAsync(RoleType roleType);
    }
}
