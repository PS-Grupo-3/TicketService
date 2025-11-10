using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventSeat
    {
        public Guid EventSeatId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventSectorId { get; set; }
        public long SeatId { get; set; }
        public Decimal Price { get; set; }
        public Guid? TicketId { get; set; } = Guid.Empty;
        public int StatusId { get; set; }

        // Relashionsships
        public Ticket? TicketRef { get; set; }
        public TicketStatus? StatusRef { get; set; }
    }
}
