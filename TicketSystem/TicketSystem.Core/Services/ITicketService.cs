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

        public Task UpdateTicketAsync(Guid id, string title, string summary, string description,
            TicketStatus ticketStatus, int severity, int priority, string account);
        public Task<List<TicketStatus>> GetTicketStatusAsync(RoleType roleType);

        public Task ResolveTicketAsync(Guid id, string account);

        public Task<List<TicketType>> GetTicketTypeAsync(RoleType roleType);
    }
}
