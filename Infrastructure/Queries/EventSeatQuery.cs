using Application.Interfaces.IEvenSeat;
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
    public class EventSeatQuery : IEventSeatQuery
    {
        private readonly AppDbContext _context;

        public EventSeatQuery(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<EventSeat?> GetEventSeatById(Guid id)
        {
            return await _context.EventSeats.FindAsync(id);
        }

        public async Task<List<EventSeat>> GetEventSeatsAllAsync()
        {
            return await _context.EventSeats.ToListAsync();
        }

        public async Task<EventSeat> GetEventSeatsByEventSectorIdAsync(Guid eventId, Guid eventSectorId, long seatId)
        {
            var query = _context.EventSeats
                .Include(ES => ES.StatusRef)
                .AsQueryable();
            query = query.Where(ES => ES.EventId == eventId && ES.EventSectorId == eventSectorId && ES.SeatId == seatId);
            return await query.FirstOrDefaultAsync();
        }
    }
}
