﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class TicketUpdateRequest
    {
        public Guid TicketId { get; set; }
        public int StatusId { get; set; }
    }
}
