﻿using AirlineManagement.Data.Context;
using AirlineManagement.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirlineContext _context;
        public UnitOfWork(AirlineContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
