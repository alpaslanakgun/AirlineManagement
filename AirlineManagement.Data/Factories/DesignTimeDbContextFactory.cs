using AirlineManagement.Data.Configurations.DatabaseConfigurations;
using AirlineManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AirlineContext>
    {
        public AirlineContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AirlineContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
