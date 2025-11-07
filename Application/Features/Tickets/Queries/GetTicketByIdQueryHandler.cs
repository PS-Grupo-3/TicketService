using Application.Features.Tickets.Models.Responses;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketStatus;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketResponse>
    {
        private readonly ITicketQuery _ticketQuery;

        public GetTicketByIdQueryHandler(ITicketQuery ticketQuery)
        {
            _ticketQuery = ticketQuery;
        }

        public async Task<TicketResponse> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _ticketQuery.GetTicketById(request.ticketId);
            if (dto == null)
            {
                throw new KeyNotFoundException($"No se encontro el ticket con ID {request.ticketId}");
            }
            return new TicketResponse
            {
                TicketId = dto.TicketId,
                UserId = dto.UserId,
                EventId = dto.EventId,
                EventSeats = dto.EventSeats.Select(item => new EventSeatResponse
                {
                    EventSeatId = item.EventSeatId,
                    EventSectorId = item.EventSectorId,
                    SeatId = item.SeatId,
                    Price = item.Price,

                }).ToList(),
                Status = new TicketStatusResponse
                {
                    StatusID = dto.StatusRef.StatusID,
                    Name = dto.StatusRef.Name,
                },
                Created = dto.Created,
                Updated = dto.Updated,
            };
        }
    }
}
