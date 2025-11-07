using Application.Interfaces.IEvenSeat;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EventSeat.Queries
{
    public class GetAllEventSeatQueryHandler : IRequestHandler<GetAllEventSeatQuery, List<EventSeatResponse>>
    {
        private readonly IEventSeatQuery _eventSeatQuery;

        public GetAllEventSeatQueryHandler(IEventSeatQuery eventSeatQuery)
        {
            this._eventSeatQuery = eventSeatQuery;
        }

        public async Task<List<EventSeatResponse>> Handle(GetAllEventSeatQuery request, CancellationToken cancellationToken)
        {
            var EventSeats = await _eventSeatQuery.GetEventSeatsAllAsync();
            if (EventSeats.Count == 0)
            {
                throw new KeyNotFoundException("No hay EventSeat creados.");
            }
            return EventSeats.Select(eventSeat => new EventSeatResponse
            {
                EventSeatId = eventSeat.EventSeatId,
                EventSectorId = eventSeat.EventSectorId,
                SeatId = eventSeat.SeatId,
                Price = eventSeat.Price,
            }).ToList();
        }
    }
}
