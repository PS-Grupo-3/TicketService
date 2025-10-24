using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketStatus
    {
        public int StatusID { get; set; }
        public string Name { get; set; }

        // Relashionsships
        public ICollection<Ticket> Tickets { get; set; }
    }
}
