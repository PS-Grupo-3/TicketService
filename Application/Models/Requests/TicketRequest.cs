using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Models.Requests
{
    public class TicketRequest
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid EventSeatId { get; set; }
        public Guid OrderId { get; set; }
        public int StatusId { get; set; }
    }
}
