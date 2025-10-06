using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _appDbContext;

        public TicketRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Ticket ticket)
        {
            _appDbContext.Tickets.Add(ticket);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _appDbContext.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetById(Guid id)
        {
            return await _appDbContext.Tickets.FindAsync(id);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _appDbContext.Tickets.Update(ticket);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
