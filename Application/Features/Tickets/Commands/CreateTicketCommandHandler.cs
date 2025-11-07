using Application.Features.Tickets.Models.Responses;
using Application.Interfaces.IEvenSeat;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketStatus;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketResponse>
    {
        private readonly ITicketCommand _ticketCommand;
        private readonly ITicketQuery _ticketQuery;
        private readonly IEventSeatCommand _eventSeatCommand;
        private readonly ITicketStatusQuery _ticketStatusQuery;

        public CreateTicketCommandHandler(ITicketCommand command, IEventSeatCommand eventCommand, ITicketStatusQuery ticketStatusQuery, ITicketQuery ticketQuery)
        {
            _ticketCommand = command;
            _eventSeatCommand = eventCommand;
            _ticketStatusQuery = ticketStatusQuery;
            _ticketQuery = ticketQuery;
        }

        public async Task<TicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            if(dto.UserId == null)
            {
                throw new ArgumentException("Debe ingresar un Id de usuario");
            }
            if(dto.EventId == null)
            {
                throw new ArgumentException("Debe ingresar un Id de evento");
            }
            if(dto.EventSeats.Count == 0)
            {
                throw new ArgumentException("Debe reservar como mínimo un asiento");
            }

            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                UserId = dto.UserId,
                EventId = dto.EventId,
                StatusId = 3, // inicia como habilitado
                StatusRef = await _ticketStatusQuery.GetTicketStatusById(3),
                EventSeats = new List<Domain.Entities.EventSeat>(),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
            await _ticketCommand.InsertTicket(ticket);

            foreach(var item in dto.EventSeats)
            {
                var eventSeat = new Domain.Entities.EventSeat
                {
                    EventSeatId = Guid.NewGuid(),
                    EventSectorId = item.EventSectorId,
                    SeatId = item.SeatId,
                    Price = item.Price,
                    TicketId = ticket.TicketId,
                };
                await _eventSeatCommand.InsertEventSeat(eventSeat);
            }

            var saveTicket = await _ticketQuery.GetTicketById(ticket.TicketId);
            return new TicketResponse
            {
                TicketId = saveTicket.TicketId,
                UserId = saveTicket.UserId,
                EventId = saveTicket.EventId,
                EventSeats = saveTicket.EventSeats.Select(item => new EventSeatResponse
                {
                    EventSeatId = item.EventSeatId,
                    EventSectorId = item.EventSectorId,
                    SeatId = item.SeatId,
                    Price = item.Price,
                }).ToList(),
                Status = new TicketStatusResponse
                {
                    StatusID = saveTicket.StatusRef.StatusID,
                    Name = saveTicket.StatusRef.Name,
                },
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
        }
    }
}
