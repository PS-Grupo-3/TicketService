using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class EventSeatTicketRequest
    {
        public Guid EventSectorId { get; set; }
        public long SeatId { get; set; }
        public Decimal Price { get; set; }
    }
}
