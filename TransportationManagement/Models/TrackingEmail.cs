using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace TransportationManagement.Models
{
    public class TrackingEmail : Email
    {
        public string To { get; set; }
        public string TrackingId { get; set; }
        public string Comment { get; set; }
    }
}