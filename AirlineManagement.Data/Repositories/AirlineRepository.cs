using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;


namespace AirlineManagement.Data.Repositories
{
    public class AirlineRepository : GenericRepository<Airline>, IAirlineRepository
    {
        public AirlineRepository(AirlineContext context) : base(context)
        {
        }
    }
}
