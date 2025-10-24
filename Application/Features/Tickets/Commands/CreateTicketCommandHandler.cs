using Application.Features.Tickets.Models.Responses;
using Application.Interfaces.ITicket;
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
        private readonly ITicketCommand _command;

        public CreateTicketCommandHandler(ITicketCommand command)
        {
            _command = command;
        }

        public async Task<TicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var ticket = new Ticket
            {
                UserId = dto.UserId,
                EventId = dto.EventId,
                EventSeatId = dto.EventSeatId,
                OrderId = dto.OrderId,
                StatusId = dto.StatusId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };

            await _command.InsertTicket(ticket);

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
                    IsAvaliable = item.IsAvaliable,

                }).ToList(),
                OrderId = dto.OrderId,
                Status = new TicketStatusResponse
                {
                    StatusID = ticket.StatusRef.StatusID,
                    Name = ticket.StatusRef.Name,
                },
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
        }
    }
}
