using Application.Interfaces.IEvenSeat;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSeat.Commands
{
    public class CreateEventSeatCommandHandler : IRequestHandler<CreateEventSeatCommand, EventSeatResponse>
    {
        private readonly IEventSeatCommand _command;

        public CreateEventSeatCommandHandler(IEventSeatCommand command)
        {
            _command = command;
        }

        public async Task<EventSeatResponse> Handle(CreateEventSeatCommand request, CancellationToken cancellationToken)
        {
            var dto = request.request;
            var EventSeat = new Domain.Entities.EventSeat
            {
                EventSectorId = dto.EventSectorId,
                SeatId = dto.SeatId,
                Price = dto.Price,
                TicketId = dto.TicketId,
            };
            await _command.InsertEventSeat(EventSeat);
            return new EventSeatResponse
            {
                EventSeatId = EventSeat.EventSeatId,
                EventSectorId = EventSeat.EventSectorId,
                Price = EventSeat.Price,
            };
        }
    }
}
