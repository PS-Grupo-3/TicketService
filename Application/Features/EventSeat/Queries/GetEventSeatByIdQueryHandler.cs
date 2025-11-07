using Application.Interfaces.IEvenSeat;
using Application.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventSeat.Queries
{
    public class GetEventSeatByIdQueryHandler : IRequestHandler<GetEventSeatByIdQuery, EventSeatResponse>
    {
        private readonly IEventSeatQuery _eventSeatQuery;

        public GetEventSeatByIdQueryHandler(IEventSeatQuery eventSeatQuery)
        {
            _eventSeatQuery = eventSeatQuery;
        }

        public async Task<EventSeatResponse> Handle(GetEventSeatByIdQuery request, CancellationToken cancellationToken)
        {
            var eventSeat = await _eventSeatQuery.GetEventSeatById(request.EventSeatId);
            if (eventSeat == null)
            {
                throw new KeyNotFoundException($"No existe el item con el id: {request.EventSeatId}");
            }
            return new EventSeatResponse
            {
                EventSeatId = eventSeat.EventSeatId,
                EventSectorId = eventSeat.EventSectorId,
                SeatId = eventSeat.SeatId,
                Price = eventSeat.Price,
            };
        }
    }
}
