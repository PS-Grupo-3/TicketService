using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.External.Requests
{
    public class EventSectorExternal
    {
        public Guid SectorId { get; set; }
        public Decimal PriceSector { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public List<SeatRequestExternal>? Seats { get; set; }
    }
}
