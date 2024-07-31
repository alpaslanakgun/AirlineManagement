using AirlineManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Contracts
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IList<Reservation>> GetReservationsByFlightNumberAsync(string flightNumber);

    }
}
