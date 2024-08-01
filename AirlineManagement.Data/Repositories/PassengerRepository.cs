using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;


namespace AirlineManagement.Data.Repositories
{
    public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(AirlineContext context) : base(context)
        {
        }
    }
}
