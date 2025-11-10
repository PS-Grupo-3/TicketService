using Application.Models.External.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class EventSeatRequest
    {
       public EventRequestExternal? Event { get; set; }
    }
}
