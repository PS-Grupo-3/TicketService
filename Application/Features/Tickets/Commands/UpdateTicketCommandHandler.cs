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
            var dto = await _ticketQuery.GetTicketById(request.Request.TicketId);
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            dto.StatusRef = await _statusQuery.GetTicketStatusById(dto.StatusId);

            await _ticketCommand.InsertTicket(dto);

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
                    IsAvaliable = item.IsAvaliable,

                }).ToList(),
                OrderId = dto.OrderId,
                Status = new TicketStatusResponse
                {
                    StatusID = dto.StatusRef.StatusID,
                    Name = dto.StatusRef.Name,
                },
                Created = dto.Created,
                Updated = DateTime.UtcNow,
            };
        }
    }
}
