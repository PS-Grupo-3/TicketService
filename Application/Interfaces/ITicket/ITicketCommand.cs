using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITicket
{
    public interface ITicketCommand
    {
        Task InsertTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
    }
}
