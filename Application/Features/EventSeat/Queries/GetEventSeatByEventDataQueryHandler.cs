using Application.Interfaces.IEvenSeat;
using Application.Models.Responses;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Application.Features.EventSeat.Queries
{
    public class GetEventSeatByEventDataQueryHandler : IRequestHandler<GetEventSeatByEventDataQuery, EventSeatResponse?>
    {
        private readonly IEventSeatQuery _eventSeatRepository;

        public GetEventSeatByEventDataQueryHandler(IEventSeatQuery eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository;
        }
              
        public async Task<EventSeatResponse?> Handle(GetEventSeatByEventDataQuery request, CancellationToken cancellationToken)
        {
            var eventSeat = await _eventSeatRepository.GetEventSeatsByEventSectorIdAsync(request.eventId, request.eventSectorId, request.seatId);
            if (eventSeat == null)
            {
                return null;
            }
            return new EventSeatResponse
            {
                EventSeatId = eventSeat.EventSeatId,
                EventId = eventSeat.EventId,
                EventSectorId = eventSeat.EventSectorId,
                SeatId = eventSeat.SeatId,
                Price = eventSeat.Price,
                TicketId = eventSeat.TicketId,
                StatusId = eventSeat.StatusId
            };
        }
    }
}
