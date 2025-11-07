using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IEvenSeat
{
    public interface IEventSeatCommand
    {
        Task InsertEventSeat(EventSeat eventSeat);
        Task UpdateEventSeat(EventSeat evetSeat);
    }
}
