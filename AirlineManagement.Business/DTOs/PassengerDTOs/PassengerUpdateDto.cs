using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.DTOs.PassengerDTOs
{
    public class PassengerUpdateDto : BaseDto
    {
        public string PassengerId { get; set; }
        public string Name { get; set; }
    }
}
