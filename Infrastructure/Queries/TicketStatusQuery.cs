using Application.Interfaces.ITicketStatus;
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
    public class TicketStatusQuery : ITicketStatusQuery
    {
        private readonly AppDbContext _context;

        public TicketStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketStatus>> GetAllTicketStatusAsync()
        {
            return await _context.TicketStatuses.ToListAsync();
        }

        public async Task<TicketStatus?> GetTicketStatusById(int statusId)
        {
            return await _context.TicketStatuses.FirstOrDefaultAsync(status => status.StatusID == statusId);
        }
    }
}
