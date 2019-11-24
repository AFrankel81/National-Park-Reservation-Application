using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {

        private string connectionString;

        // Single Parameter Constructor
        public ParkSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> ListAllParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park;", connection);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.Park_Id = Convert.ToInt32(reader["Park_Id"]);
                        park.Name = Convert.ToString(reader["Name"]);
                        park.Location = Convert.ToString(reader["Location"]);
                        park.Establish_Date = Convert.ToDateTime(reader["Establish_Date"]);
                        park.Area = Convert.ToInt32(reader["Area"]);
                        park.Visitors = Convert.ToInt32(reader["Visitors"]);
                        park.Description = Convert.ToString(reader["Description"]);

                    

                        parks.Add(park);
                    }
                    return parks;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return parks;

        }

        //Display park information
        public string DisplayParkInformation(int park_id)
        {
            Park park = new Park();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM park where park_id = @park_id", connection);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        park.Park_Id = Convert.ToInt32(reader["Park_Id"]);
                        park.Name = Convert.ToString(reader["Name"]);
                        park.Location = Convert.ToString(reader["Location"]);
                        park.Establish_Date = Convert.ToDateTime(reader["Establish_Date"]);
                        park.Area = Convert.ToInt32(reader["Area"]);
                        park.Visitors = Convert.ToInt32(reader["Visitors"]);
                        park.Description = Convert.ToString(reader["Description"]);
                    }
                    return $"Park Information Screen\nName:\t\t{park.Name}\nLocation:\t{park.Location}\nEstablished:\t{park.Establish_Date}\nArea:\t\t{park.Area}\nAnnual Visitors:{park.Visitors}\n\n{park.Description}";
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return $"nothing";
        }
    }
}
