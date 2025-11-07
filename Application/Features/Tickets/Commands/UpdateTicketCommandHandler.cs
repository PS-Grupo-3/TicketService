using Application.Features.Tickets.Models.Responses;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketStatus;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketResponse>
    {
        private readonly ITicketCommand _ticketCommand;
        private readonly ITicketQuery _ticketQuery;
        private readonly ITicketStatusQuery _statusQuery;

        public UpdateTicketCommandHandler(ITicketCommand command, ITicketQuery query, ITicketStatusQuery statusQuery)
        {
            _ticketCommand = command;
            _ticketQuery = query;
            _statusQuery = statusQuery;
        }

        public async Task<TicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketQuery.GetTicketById(request.id);
            if (ticket == null)
            {
                throw new ArgumentNullException($"El Ticket no existe para el Id ingresado: {request.id}");
            }

            var statusRef = await _statusQuery.GetTicketStatusById(request.Request.StatusId);
            if (statusRef == null)
            {
                throw new ArgumentNullException($"El estado no existe para el Id ingresado: {request.id}");
            }

            ticket.StatusRef = statusRef;
            ticket.StatusId = statusRef.StatusID;
            ticket.Updated = DateTime.UtcNow;
            await _ticketCommand.UpdateTicket(ticket);

            return new TicketResponse
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
                Updated = DateTime.UtcNow,
            };
        }
    }
}
