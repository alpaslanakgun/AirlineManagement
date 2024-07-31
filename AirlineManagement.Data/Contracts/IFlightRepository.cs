﻿using AirlineManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Contracts
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        IQueryable<Flight> GetFlightsMatchingSearch(string search);
    }
}
