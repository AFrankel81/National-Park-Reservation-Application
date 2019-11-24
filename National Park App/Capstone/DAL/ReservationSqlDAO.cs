using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationSqlDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ReservationSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        bool sitesFound;

        public string SearchReservation(int selectedCampgroundID, DateTime selectedFromDate, DateTime selectedToDate)
        {
            List<int> bookedSiteIDs = new List<int>();
            string confirmationMessage;


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM campground c JOIN park p ON c.park_id = p.park_id JOIN site s on s.campground_id = c.campground_id left join reservation r on r.site_id = s.site_id WHERE c.campground_id = {selectedCampgroundID} AND r.from_date >= '{selectedFromDate}' and r.to_date <= '{selectedToDate}'", connection);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Reservation_Id = Convert.ToInt32(reader["Reservation_Id"]);
                        reservation.Site_Id = Convert.ToInt32(reader["Site_Id"]);
                        reservation.Name = Convert.ToString(reader["Name"]);
                        reservation.From_Date = Convert.ToDateTime(reader["From_Date"]);
                        reservation.To_Date = Convert.ToDateTime(reader["To_Date"]);
                        reservation.Create_Date = Convert.ToDateTime(reader["Create_Date"]);

                        bookedSiteIDs.Add(reservation.Site_Id);

                        if (reader.HasRows)
                        {
                            sitesFound = true;
                        }

                    }

                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            if (sitesFound == true)
            {

                confirmationMessage = "Sorry! No reservations available for that date range!";

                return confirmationMessage;
            }
            else
            {
                confirmationMessage = "Results Matching Your Search Criteria";
                return confirmationMessage;
            }


        }

        public List<Site> FindOpenSites(int selectedCampgroundID, DateTime selectedFromDate, DateTime selectedToDate)
        {
            List<Site> openSites = new List<Site>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"select * from campground c join site s on c.campground_id = s.campground_id Where c.campground_id = @selectedCampgroundID AND s.site_id NOT IN( select s.site_id from campground c join park p on c.park_id = p.park_id join site s on s.campground_id = c.campground_id right join reservation r on r.site_id = s.site_id where c.campground_id = @selectedCampgroundID AND ((r.from_date >= '{selectedFromDate}' AND r.to_date <= '{selectedToDate}') OR ('{selectedFromDate}' >= r.from_date AND '{selectedFromDate}' <= r.to_date) OR ('{selectedToDate}' >= r.from_date AND '{selectedToDate}' <= r.to_date)))", connection);
                    cmd.Parameters.AddWithValue("@selectedCampgroundID", selectedCampgroundID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.RName = Convert.ToString(reader["name"]);
                        site.SDaily_Fee = decimal.Parse(Convert.ToString(reader["daily_fee"]));
                        site.Site_Id = Convert.ToInt32(reader["Site_Id"]);
                        site.Campground_Id = Convert.ToInt32(reader["Campground_Id"]);
                        site.Site_Number = Convert.ToInt32(reader["Site_Number"]);
                        site.Max_Occupancy = Convert.ToInt32(reader["Max_Occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["Accessible"]);
                        site.Max_Rv_Length = Convert.ToInt32(reader["Max_Rv_Length"]);
                        site.Utilites = Convert.ToBoolean(reader["Utilities"]);

                        openSites.Add(site);
                    }
                    return openSites;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return openSites;

        }

        public int MakeReservation(DateTime reserveFromDate, DateTime reserveToDate)
        {
            int newID;
            int siteToReserve;
            string reservationName;
            Console.WriteLine("Which site should be reserved (enter 0 to cancel)?");
            siteToReserve = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("What name should the reservation be made under?");
            reservationName = Console.ReadLine();
            Console.WriteLine();


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    SqlCommand cmd = new SqlCommand($"INSERT INTO reservation(site_id, name, from_date, to_date) VALUES (@siteToReserve, @reservationName, @reserveFromDate, @reserveToDate);", connection);

                    cmd.Parameters.AddWithValue("@siteToReserve", siteToReserve);
                    cmd.Parameters.AddWithValue("@reservationName", reservationName);
                    cmd.Parameters.AddWithValue("@reserveFromDate", reserveFromDate);
                    cmd.Parameters.AddWithValue("@reserveToDate", reserveToDate);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("Select max(reservation_id) from reservation;", connection);
                    newID = Convert.ToInt32(cmd2.ExecuteScalar());
                    return newID;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
         
        }

        public bool MakeSureCampgroundIsOpen(DateTime selectedFromDate, DateTime selectedToDate, int selectedCampgroundID)
        {

            Campground campground4 = new Campground();
            try
            {
                using (SqlConnection connection4 = new SqlConnection(connectionString))
                {
                    connection4.Open();

                    SqlCommand cmd4 = new SqlCommand($"SELECT * FROM campground where campground_id = @selectedCampgroundID", connection4);
                    cmd4.Parameters.AddWithValue("@selectedCampgroundID", selectedCampgroundID);

                    SqlDataReader reader4 = cmd4.ExecuteReader();


                    while (reader4.HasRows)
                    {
                        campground4.Campground_Id = Convert.ToInt32(reader4["campground_id"]);
                        campground4.Park_Id = Convert.ToInt32(reader4["park_id"]);
                        campground4.Name = Convert.ToString(reader4["name"]);
                        campground4.Open_From_MM = Convert.ToInt32(reader4["open_from_mm"]);
                        campground4.Open_To_MM = Convert.ToInt32(reader4["open_to_mm"]);
                        campground4.Daily_Fee = Convert.ToDecimal(reader4["daily_fee"]);

                    }
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            if (selectedFromDate.Month >= campground4.Open_From_MM && selectedToDate.Month <= campground4.Open_To_MM)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
