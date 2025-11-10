using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class SeatTicketRequest
    {
        public Guid EventId { get; set; }
        public Guid EventSectorId { get; set; }
        public long SeatId { get; set; }
    }
}
