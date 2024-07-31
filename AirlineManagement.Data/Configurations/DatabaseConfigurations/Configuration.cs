using Microsoft.Extensions.Configuration;

namespace AirlineManagement.Data.Configurations.DatabaseConfigurations
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AirlineManagement.Presentation"));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Production.json");
                }

                return configurationManager.GetConnectionString("SqlConnection");
            }
        }

    }
}
