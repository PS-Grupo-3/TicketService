﻿using Application.Features.Tickets.Commands;
using Application.Features.Tickets.Models.Requests;
using Application.Features.Tickets.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketRequest request)
        {
            var result = await _mediator.Send(new CreateTicketCommand(request));
            return CreatedAtAction(nameof(GetTicketById), new { id = result.TicketId }, result);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(TicketUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateTicketCommand(request));
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTicketById(Guid Id)
        {
            var result = await _mediator.Send(new GetTicketByIdQuery(Id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlltickets()
        {
            var items = await _mediator.Send(new GetAllTicketsQuery());
            return Ok(items);
        }
    }
}
