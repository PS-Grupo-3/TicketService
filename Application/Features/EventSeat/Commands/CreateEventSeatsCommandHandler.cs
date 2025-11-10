using Application.Interfaces.IEvenSeat;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSeat.Commands
{
    public class CreateEventSeatsCommandHandler : IRequestHandler<CreateEventSeatCommand, EventSeatsResponse>
    {
        private readonly IEventSeatCommand _command;

        public CreateEventSeatsCommandHandler(IEventSeatCommand command)
        {
            _command = command;
        }

        public async Task<EventSeatsResponse> Handle(CreateEventSeatCommand request, CancellationToken cancellationToken)
        {
            var contador = 0;
            var dto = request.request.Event;
            if(dto.EventId == Guid.Empty)
            {
                throw new ArgumentException("El evento no existe, verifique el ID ingresado");
            }
            if(dto.EventSectors == null || dto.EventSectors.Count == 0)
            {
                throw new ArgumentException("El evento no tiene sectores asociados");
            }
            foreach (var sector in dto.EventSectors)
            {
                if (sector.Available)
                {
                    if(sector.Seats == null || sector.Seats.Count == 0)
                    {
                        throw new ArgumentException($"El sector {sector.SectorId} no tiene asientos asociados");
                    }
                    foreach (var seat in sector.Seats)
                    {
                        var EventSeat = new Domain.Entities.EventSeat
                        {
                            EventSeatId = Guid.NewGuid(),
                            EventId = dto.EventId,
                            EventSectorId = sector.SectorId,
                            SeatId = seat.SeatId,
                            Price = sector.PriceSector,
                            StatusId = 1 // los asiento de un evento inician "disponibles"
                        };
                        await _command.InsertEventSeat(EventSeat);
                        contador++;
                    }
                }
            }

            return new EventSeatsResponse
            {
                Message = $"{contador} asientos creados para el evento {dto.EventId}"
            };
        }
    }
}
