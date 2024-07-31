using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using AirlineManagement.Domain.Entities;
using AirlineManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly AirlineContext _dbContext;

        public ReservationRepository(AirlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IList<Reservation>> GetReservationsByFlightNumberAsync(string flightNumber)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                throw new ArgumentException("Flight number must be provided.", nameof(flightNumber));
            }

            return await _dbContext.Reservations.Where(r => r.FlightNumber == flightNumber).ToListAsync();
        }
    }
}
