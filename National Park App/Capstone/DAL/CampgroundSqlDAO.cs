using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundSqlDAO
    {
        private string connectionString;

        public int mmto;
        public int mmfrom;

        // Single Parameter Constructor
        public CampgroundSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Campground> ListAllCampgrounds(int park_id)
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM campground WHERE park_id = @park_id;", connection);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.Campground_Id = Convert.ToInt32(reader["Campground_Id"]);
                        campground.Name = Convert.ToString(reader["Name"]);
                        campground.Open_From_MM = Convert.ToInt32(reader["Open_From_MM"]);
                        campground.Open_To_MM = Convert.ToInt32(reader["Open_To_MM"]);
                        campground.Daily_Fee = Convert.ToDecimal(reader["Daily_Fee"]);

                        campgrounds.Add(campground);
                    }
                    return campgrounds;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return campgrounds;

        }

        //Display park information
        public string DisplayCampgroundInformation(int park_id)
        {
            Campground campground = new Campground();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM campground where campground_id = @park_id", connection);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.HasRows)
                    {
                        campground.Campground_Id = Convert.ToInt32(reader["Campground_Id"]);
                        campground.Name = Convert.ToString(reader["Name"]);
                        campground.Open_From_MM = Convert.ToInt32(reader["Open_From_MM"]);
                        campground.Open_To_MM = Convert.ToInt32(reader["Open_To_MM"]);
                        campground.Daily_Fee = Convert.ToDecimal(reader["Daily_Fee"]);

                        Console.WriteLine($"Campground ID{campground.Campground_Id} Name {campground.Name} Open From {campground.Open_From_MM} Closed {campground.Open_To_MM} Daily Fee {campground.Daily_Fee}");
                    }
                    return $"";
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

