using Application.Features.Tickets.Models.Requests;
using Application.Features.Tickets.Models.Responses;
using Application.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Commands
{
    public record UpdateTicketCommand(Guid id, TicketUpdateRequest Request) : IRequest<TicketResponse>;
}
