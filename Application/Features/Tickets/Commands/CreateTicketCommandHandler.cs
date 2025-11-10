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
        private readonly IEventSeatQuery _eventSeatQuery;
        private readonly ITicketStatusQuery _ticketStatusQuery;

        public CreateTicketCommandHandler(ITicketCommand command, IEventSeatCommand eventCommand, IEventSeatQuery eventSeatQuery, ITicketStatusQuery ticketStatusQuery, ITicketQuery ticketQuery)
        {
            _ticketCommand = command;
            _eventSeatCommand = eventCommand;
            _eventSeatQuery = eventSeatQuery;
            _ticketStatusQuery = ticketStatusQuery;
            _ticketQuery = ticketQuery;
        }

        public async Task<TicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            if(dto.UserId == Guid.Empty)
            {
                throw new ArgumentException("Debe ingresar un Id de usuario");
            }
            if(dto.Seats == null || dto.Seats.Count == 0)
            {
                throw new ArgumentException("Debe ingresar al menos un asiento para crear el ticket");
            }
            // crear el ticket
            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                UserId = dto.UserId,
                EventId = dto.EventId,
                StatusId = 3, // inicia como disponible
                StatusRef = await _ticketStatusQuery.GetTicketStatusById(3),
                EventSeats = new List<Domain.Entities.EventSeat>(),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
            // agregar los asientos al ticket y actualizar el estado de los asientos
            foreach (var seat in dto.Seats)
            {
                var eventSeat = await _eventSeatQuery.GetEventSeatsByEventSectorIdAsync(seat.EventId, seat.EventSectorId, seat.SeatId);
                eventSeat.StatusId = 2; // reservado
                await _eventSeatCommand.UpdateEventSeat(eventSeat);
                ticket.EventSeats.Add(eventSeat);
            }
            // cargo el ticket en la base de datos
            await _ticketCommand.InsertTicket(ticket);

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
