using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace AirlineManagement.Data.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly AirlineContext _dbContext;

        public FlightRepository(AirlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public IQueryable<Flight> GetFlightsMatchingSearch(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                throw new ArgumentException("Search term must be provided.", nameof(search));
            }

            return _dbContext.Flights.Where(f =>
               f.FlightNumber.Contains(search) ||
               f.DepartureAirport.Contains(search) ||
               f.ArrivalAirport.Contains(search) ||
               f.AircraftId.Contains(search));
        }
    }
}
