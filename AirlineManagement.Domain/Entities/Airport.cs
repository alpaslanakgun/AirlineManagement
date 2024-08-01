using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class Airport: IEntity
    {
        public string AirportCode { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Flight> DepartureFlights { get; set; } 
        public ICollection<Flight> ArrivalFlights { get; set; }
    }
}
