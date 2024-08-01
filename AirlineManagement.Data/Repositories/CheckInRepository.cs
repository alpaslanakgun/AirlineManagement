using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Repositories
{
    public class CheckInRepository : GenericRepository<CheckIn>, ICheckInRepository
    {
        public CheckInRepository(AirlineContext context) : base(context)
        {
        }
    }
}
