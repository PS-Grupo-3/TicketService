using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.External.Requests
{
    public class EventRequestExternal
    {
        public Guid EventId { get; set; } = Guid.Empty;
        public List<EventSectorExternal>? EventSectors { get; set; }
    }
}
