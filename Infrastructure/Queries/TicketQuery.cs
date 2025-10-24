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

        public async Task<List<Ticket>> GetTicketAllAsync()
        {
            return await _context.Tickets
                .Include(t => t.StatusRef)
                .Include(t => t.EventSeats)
                .ToListAsync();
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
