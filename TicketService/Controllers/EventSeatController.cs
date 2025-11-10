using Application.Features.EventSeat.Commands;
using Application.Features.EventSeat.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventSeatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventSeatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventSeats(EventSeatRequest request)
        {
            var result = await _mediator.Send(new CreateEventSeatCommand(request));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventSeats([FromQuery] Guid eventId, [FromQuery] Guid eventSectorId, [FromQuery] long seatId)
        {
            var item = await _mediator.Send(new GetEventSeatByEventDataQuery(eventId, eventSectorId, seatId));
            return Ok(item);
        }
    }
}
