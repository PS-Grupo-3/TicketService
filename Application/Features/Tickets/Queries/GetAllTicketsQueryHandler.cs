using Application.Features.Tickets.Models.Responses;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketStatus;
using Application.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketResponse>>
    {
        private readonly ITicketQuery _ticketQuery;

        public GetAllTicketsQueryHandler(ITicketQuery ticketQuery)
        {
            _ticketQuery = ticketQuery;
        }

        public async Task<List<TicketResponse>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketQuery.GetTicketAllAsync(request.eventId, request.userId);
            return tickets.Select(ticket => new TicketResponse
            {
                TicketId = ticket.TicketId,
                UserId = ticket.UserId,
                EventId = ticket.EventId,
                EventSeats = ticket.EventSeats.Select(item => new EventSeatResponse
                {
                    EventSeatId = item.EventSeatId,
                    EventSectorId = item.EventSectorId,
                    SeatId = item.SeatId,
                    Price = item.Price,

                }).ToList(),
                Status = new TicketStatusResponse
                {
                    StatusID = ticket.StatusRef.StatusID,
                    Name = ticket.StatusRef.Name,
                },
                Created = ticket.Created,
                Updated = ticket.Updated,
            }).ToList();
        }
    }
}
