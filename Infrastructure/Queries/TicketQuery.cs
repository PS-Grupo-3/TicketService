using Application.Interfaces.ITicket;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class TicketQuery : ITicketQuery
    {
        private readonly AppDbContext _context;

        public TicketQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetTicketAllAsync(Guid? eventId, Guid? userId)
        {
            var query=  _context.Tickets
                .Include(t => t.StatusRef)
                .Include(t => t.EventSeats)
                .AsQueryable();

            if (eventId.HasValue)
            {
                query = query.Where(t => t.EventId == eventId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(t => t.UserId == userId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Ticket?> GetTicketById(Guid ticketID)
        {
            return await _context.Tickets
                .Include(t => t.StatusRef)
                .Include(t => t.EventSeats)
                .FirstOrDefaultAsync(t => t.TicketId == ticketID);
        }
    }
}
