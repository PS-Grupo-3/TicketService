using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid EventId { get; set; } = Guid.Empty;
        public int StatusId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        // Relationships
        public TicketStatus StatusRef { get; set; }
        public ICollection<EventSeat> EventSeats { get; set; }
    }
}
