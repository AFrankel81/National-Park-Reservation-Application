using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public SiteSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        List<Site> sites = new List<Site>();
        public IList<Site> ListAllSites(int campground_id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Site WHERE campground_id = @campground_id;", connection);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Site site = new Site();

                        site.Site_Id = Convert.ToInt32(reader["Site_Id"]);
                        site.Campground_Id = Convert.ToInt32(reader["Campground_Id"]);
                        site.Site_Number = Convert.ToInt32(reader["Site_Number"]);
                        site.Max_Occupancy = Convert.ToInt32(reader["Max_Occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["Accessible"]);
                        site.Max_Rv_Length = Convert.ToInt32(reader["Max_Rv_Length"]);
                        site.Utilites = Convert.ToBoolean(reader["Utilities"]);

                        sites.Add(site);
                    }
                    return sites;
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return sites;

        }

        public string DisplaySiteInformation(int campground_id, DateTime selectedFromDate, DateTime selectedToDate)
        {
            Site site = new Site();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM site s LEFT JOIN campground c ON c.campground_id = site.campground_id WHERE s.campground_id = @campground_id", connection);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        {
                            site.Site_Id = Convert.ToInt32(reader["Site_Id"]);
                            site.Campground_Id = Convert.ToInt32(reader["Campground_Id"]);
                            site.Site_Number = Convert.ToInt32(reader["Site_Number"]);
                            site.Max_Occupancy = Convert.ToInt32(reader["Max_Occupancy"]);
                            site.Accessible = Convert.ToBoolean(reader["Accessible"]);
                            site.Max_Rv_Length = Convert.ToInt32(reader["Max_Rv_Length"]);
                            site.Utilites = Convert.ToBoolean(reader["Utilities"]);
                        }

                    }
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

