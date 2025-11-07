using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicket
{
    public interface ITicketQuery
    {
        Task<List<Ticket>> GetTicketAllAsync(Guid? eventId, Guid? userId);
        Task<Ticket?> GetTicketById(Guid ticketID);
    }
}
