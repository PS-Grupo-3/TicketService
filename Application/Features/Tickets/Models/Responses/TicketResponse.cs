using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Models.Responses
{
    public class TicketResponse
    {
        public Guid TicketId { get; set; }
        public Guid EventId { get; set; }
        public Guid SectorId { get; set; }
        public Guid OrderId { get; set; }
        public long? SeaId { get; set; }
        public int StatusId { get; set; }
        public DateTime Created { get; set; }
    }
}
