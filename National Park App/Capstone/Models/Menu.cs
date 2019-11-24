using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Linq;
using System.Threading;

namespace Capstone.Models
{
    public class Menu
    {
        public IParkSqlDAO parkDAO;
        public ICampgroundSqlDAO campgroundDAO;
        public ISiteSqlDAO siteDAO;
        public IReservationSqlDAO reservationDAO;

        public Menu(IParkSqlDAO parkDAO, ICampgroundSqlDAO campgroundDAO, ISiteSqlDAO siteDAO, IReservationSqlDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
        }

        public int parkID;
        public int campgroundID;
        public int siteID;
        public int reservationID;
        public DateTime selectedFromDate;
        public DateTime selectedToDate;
        public int selectedCampgroundID;

        public void Header()
        {
            Console.WriteLine(@"  ____            _      ____                                _   _               ____            _                 ");
            Console.WriteLine(@" |  _ \ __ _ _ __| | __ |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __   / ___| _   _ ___| |_ ___ _ __ ___  ");
            Console.WriteLine(@" | |_) / _` | '__| |/ / | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \  \___ \| | | / __| __/ _ \ '_ ` _ \ ");
            Console.WriteLine(@" |  __/ (_| | |  |   <  |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | |  ___) | |_| \__ \ ||  __/ | | | | |");
            Console.WriteLine(@" |_|   \__,_|_|  |_|\_\ |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_| |____/ \__, |___/\__\___|_| |_| |_|");
            Console.WriteLine(@"                                                                                       |___/                       ");
        }

        public void MainMenu()
        {

            Console.WriteLine("Select a Park for Further Details");

            IList<Park> parks = parkDAO.ListAllParks();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine($"{park.Park_Id}) {park.Name.ToString().PadRight(10)}");
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }

            Console.WriteLine("4) Quit");


            int caseSwitch = int.Parse(Console.ReadLine());
            parkID = caseSwitch;
            switch (caseSwitch)
            {
                case 1:
                    Console.Clear();
                    Header();
                    Console.WriteLine(parkDAO.DisplayParkInformation(caseSwitch));
                    Console.WriteLine();
                    SubMenu();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Header();
                    Console.WriteLine(parkDAO.DisplayParkInformation(caseSwitch));
                    Console.WriteLine();
                    SubMenu();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Header();
                    Console.WriteLine(parkDAO.DisplayParkInformation(caseSwitch));
                    Console.WriteLine();
                    SubMenu();
                    Console.ReadKey();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }

        public void SubMenu()
        {
            Console.WriteLine("Select a command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("3) Return to Previous Screen");

            Dictionary<int, string> Months = new Dictionary<int, string>();
            Months.Add(1, "January");
            Months.Add(2, "February");
            Months.Add(3, "March");
            Months.Add(4, "April");
            Months.Add(5, "May");
            Months.Add(6, "June");
            Months.Add(7, "July");
            Months.Add(8, "August");
            Months.Add(9, "September");
            Months.Add(10, "October");
            Months.Add(11, "November");
            Months.Add(12, "December");

            int caseSwitch2 = int.Parse(Console.ReadLine());
            campgroundID = caseSwitch2;
            switch (caseSwitch2)
            {

                case 1:

                    Console.Clear();
                    Header();
                    Console.WriteLine($"\tName\t\t\t\tOpen\t\tCloses\t\tDaily Fee");

                    IList<Campground> campgrounds = campgroundDAO.ListAllCampgrounds(parkID);

                    if (campgrounds.Count > 0)
                    {
                        foreach (Campground campground in campgrounds)
                        {
                            Console.WriteLine($"#{campground.Campground_Id}\t{campground.Name.PadRight(30)}\t{Months[campground.Open_From_MM].PadRight(16)}{Months[campground.Open_To_MM].PadRight(16)}{campground.Daily_Fee.ToString("C2")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("**** NO RESULTS ****");
                    }

                    Console.WriteLine();
                    SubMenu3();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Header();
                    Console.WriteLine($"\tName\t\t\t\tOpen\t\tCloses\t\tDaily Fee");

                    IList<Campground> campgrounds2 = campgroundDAO.ListAllCampgrounds(parkID);

                    if (campgrounds2.Count > 0)
                    {
                        foreach (Campground campground in campgrounds2)
                        {
                            Console.WriteLine($"#{campground.Campground_Id}\t{campground.Name.PadRight(30)}\t{Months[campground.Open_From_MM].PadRight(16)}{Months[campground.Open_To_MM].PadRight(16)}{campground.Daily_Fee.ToString("C2")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("**** NO RESULTS ****");
                    }

                    Console.WriteLine();
                    SubMenu2();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Header();
                    MainMenu();
                    break;
            }
        }


        public void SubMenu2()
        {

            Console.WriteLine();
            Console.WriteLine("Which campground (enter 0 to cancel)?");
            selectedCampgroundID = int.Parse(Console.ReadLine());

            //got back to main screen if 0
            if (selectedCampgroundID == 0)
            {
                Console.Clear();
                Header();
                MainMenu();
            }


            Console.WriteLine("What is the arrival date? (YYYY-MM-DD)");
            selectedFromDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("What is the departure date?(YYYY-MM-DD)");
            selectedToDate = DateTime.Parse(Console.ReadLine());

            bool allowBooking = reservationDAO.MakeSureCampgroundIsOpen(selectedFromDate, selectedToDate, selectedCampgroundID);



            Console.WriteLine();
            //if (allowBooking)
            //{
                Reservation newReservation = new Reservation();
                Console.WriteLine();
                List<Site> pleaseWork = new List<Site>();
                pleaseWork = reservationDAO.FindOpenSites(selectedCampgroundID, selectedFromDate, selectedToDate);
                string accessability;
                string utility;
                if (pleaseWork.Count > 0)
                {
                    Console.WriteLine("Site Name\tSite Number\tMax Occupancy\tAccessible\tMax RV Length\tUtilities\tCost");
                    foreach (Site site in pleaseWork)
                    {
                        if (site.Accessible == true)
                        {
                            accessability = "Yes";
                        }
                        else
                        {
                            accessability = "No";
                        }
                        if (site.Utilites == true)
                        {
                            utility = "Yes";
                        }
                        else
                        {
                            utility = "No";
                        }

                        Reservation thisReservation = new Reservation();

                        double daysDiff = (selectedToDate - selectedFromDate).TotalDays;
                        decimal cost = ((decimal)daysDiff * site.SDaily_Fee);

                        Console.WriteLine($"{site.RName}\t{site.Site_Number.ToString()}\t\t{site.Max_Occupancy}\t\t{accessability}\t\t{site.Max_Rv_Length}\t\t{utility}\t\t{cost:C}");
                    }

                    Console.WriteLine("");
                int reservationID = reservationDAO.MakeReservation(selectedFromDate, selectedToDate);
                Console.WriteLine($"The reservation has been made and the confirmation id is {reservationID}.");
            }
                else
                {
                    Console.WriteLine("Sorry, nothing available for these dates.");
                }

                Console.WriteLine();
                Console.ReadKey();
            Console.Clear();
            Header();
            MainMenu();
                
            }
            //else
            //{
            //    Console.WriteLine("Sorry, the park is closed for these dates.");
            //}
            
        //}

        public void SubMenu3()
        {
            Console.WriteLine("Select a command");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");

            int caseSwitch2 = int.Parse(Console.ReadLine());
            campgroundID = caseSwitch2;
            switch (caseSwitch2)
            {

                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    Console.Clear();
                    Header();
                    Dictionary<int, string> Months = new Dictionary<int, string>();
                    Months.Add(1, "January");
                    Months.Add(2, "February");
                    Months.Add(3, "March");
                    Months.Add(4, "April");
                    Months.Add(5, "May");
                    Months.Add(6, "June");
                    Months.Add(7, "July");
                    Months.Add(8, "August");
                    Months.Add(9, "September");
                    Months.Add(10, "October");
                    Months.Add(11, "November");
                    Months.Add(12, "December");
                    Console.WriteLine($"\tName\t\t\t\tOpen\t\tCloses\t\tDaily Fee");
                    Console.WriteLine();

                    IList<Campground> campgrounds2 = campgroundDAO.ListAllCampgrounds(parkID);

                    if (campgrounds2.Count > 0)
                    {
                        foreach (Campground campground in campgrounds2)
                        {
                            Console.WriteLine($"#{campground.Campground_Id}\t{campground.Name.PadRight(30)}\t{Months[campground.Open_From_MM].PadRight(16)}{Months[campground.Open_To_MM].PadRight(16)}{campground.Daily_Fee.ToString("C2")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("**** NO RESULTS ****");
                    }
                    SubMenu2();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Header();
                    SubMenu();
                    Console.ReadKey();
                    break;
            }
        }
    }
}
