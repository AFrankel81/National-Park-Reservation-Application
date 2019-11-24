using Capstone.DAL;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            IParkSqlDAO parkDAO = new ParkSqlDAO(connectionString);
            ICampgroundSqlDAO campgroundDAO = new CampgroundSqlDAO(connectionString);
            ISiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);
            IReservationSqlDAO reservationDAO = new ReservationSqlDAO(connectionString);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Menu thisMenu = new Menu(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            thisMenu.Header();
            thisMenu.MainMenu();

            Console.ReadKey();
        }


    }
}
