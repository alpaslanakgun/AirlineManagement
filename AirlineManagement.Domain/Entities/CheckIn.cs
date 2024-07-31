using AirlineManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Domain.Entities
{
    public class CheckIn : BaseEntity, IEntity
    {
        public string CheckInId { get; set; }
        public string ReservationId { get; set; }
        public string Status { get; set; }
        public int BaggageCount { get; set; }
        public DateTime BoardingTime { get; set; }
    }
}
