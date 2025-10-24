using Application.Features.Tickets.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries
{
    public record GetAllTicketsQuery() : IRequest<List<TicketResponse>>;
}
