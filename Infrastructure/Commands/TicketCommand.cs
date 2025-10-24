using Application.Interfaces.ITicket;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class TicketCommand : ITicketCommand
    {
        private readonly AppDbContext _context;

        public TicketCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
           await _context.SaveChangesAsync();
        }
    }
}
