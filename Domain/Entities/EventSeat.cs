﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventSeat
    {
        public Guid EventSeatId { get; set; }
        public Guid EventSectorId { get; set; }
        public long SeatId { get; set; }
        public Decimal Price { get; set; }
        public bool IsAvaliable { get; set; }
        public Guid TicketId { get; set; }

        // Relashionsships
        public Ticket TicketRef { get; set; }
    }
}
