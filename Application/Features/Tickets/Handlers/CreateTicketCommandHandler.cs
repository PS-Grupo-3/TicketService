using Application.Features.Tickets.Commands;
using Application.Features.Tickets.Models.Responses;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var ticket = new Ticket
            {
                EventId = dto.EventId,
                SectorId = dto.SectorId,
                OrderId = dto.OrderId,
                SeatId = dto.SeaId,
                StatusId = dto.StatusId,
                Created = DateTime.UtcNow,
            };

            await _ticketRepository.AddAsync(ticket);

            return new TicketResponse
            {
                TicketId = ticket.TicketId,
                EventId = dto.EventId,
                SectorId = dto.SectorId,
                OrderId = dto.OrderId,
                SeaId = dto.SeaId,
                StatusId = dto.StatusId,
                Created = DateTime.UtcNow,
            };
        }
    }
}
