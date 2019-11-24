using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private TransactionScope transaction;
        const string connectionString = "Server=.\\SQLEXPRESS;Database=npcampground;Trusted_Connection=True;";

        [TestInitialize]
        public void Setup()
        {
            // Begin a transaction
            this.transaction = new TransactionScope();
            string script;
            // Load a script file to setup the db the way we want it
            using (StreamReader sr = new StreamReader(@"C:\Users\KKimes\git\c-module-2-capstone-team-5\09_Capstone\Capstone.Tests\test_setup.sql"))
            {
                script = sr.ReadToEnd();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(script, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            this.transaction.Dispose();
        }
        [TestMethod]
        public void ListAllCampgroundsAcadia()
        {
            //arrange
            CampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);

            //act
            IList<Campground> campground = dao.ListAllCampgrounds(1);

            //assert
            Assert.AreEqual(3, campground.Count);
        }
        [TestMethod]
        public void ListAllCampgroundsArches()
        {
            //arrange
            CampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);

            //act
            IList<Campground> campground = dao.ListAllCampgrounds(2);

            //assert
            Assert.AreEqual(3, campground.Count);
        }
        [TestMethod]
        public void ListAllCampgroundsCuyahogaValley()
        {
            //arrange
            CampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);

            //act
            IList<Campground> campground = dao.ListAllCampgrounds(3);

            //assert
            Assert.AreEqual(1, campground.Count);
        }
        [TestMethod]
        public void ListAllParks()
        {
            //arrange
            ParkSqlDAO dao = new ParkSqlDAO(connectionString);

            //act
            IList<Park> park = dao.ListAllParks();

            //assert
            Assert.AreEqual(3, park.Count);
        }
        [TestMethod]
        public void ListAllSitesBlackwoods()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(1);

            //assert
            Assert.AreEqual(12, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesSeawall()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(2);

            //assert
            Assert.AreEqual(12, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesSchoodic()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(3);

            //assert
            Assert.AreEqual(12, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesDevils()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(4);

            //assert
            Assert.AreEqual(8, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesCanyon()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(5);

            //assert
            Assert.AreEqual(1, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesJunipers()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(6);

            //assert
            Assert.AreEqual(1, sites.Count);
        }
        [TestMethod]
        public void ListAllSitesUnnamed()
        {
            //arrange
            SiteSqlDAO siteDAO = new SiteSqlDAO(connectionString);

            //act
            IList<Site> sites = siteDAO.ListAllSites(7);

            //assert
            Assert.AreEqual(5, sites.Count);
        }
        [TestMethod]
        public void FindOpenSites()
        {
            //arrange
            ReservationSqlDAO reservationDAO = new ReservationSqlDAO(connectionString);
            DateTime selectedFromDate = Convert.ToDateTime("2019 - 01 - 01");
            DateTime selectedToDate = Convert.ToDateTime("2019 - 12 - 31");
            int selectedCampgroundID = 1;

            //act
            IList<Site> sites = reservationDAO.FindOpenSites(selectedCampgroundID, selectedFromDate, selectedToDate);
            int actualResult = sites.Count;

            //assert
            Assert.AreEqual(actualResult, 5);
        }
        [TestMethod]
        public void SearchReservationIsBooked()
        {
            //arrange
            ReservationSqlDAO reservationDAO = new ReservationSqlDAO(connectionString);
            DateTime selectedFromDate = Convert.ToDateTime("2000-01-01");
            DateTime selectedToDate = Convert.ToDateTime("2030-01-01");
            int selectedCampgroundID = 4;

            //act
            string actualResult = reservationDAO.SearchReservation(selectedCampgroundID, selectedFromDate, selectedToDate);

            //assert
            Assert.AreEqual("Sorry! No reservations available for that date range!", actualResult);
        }
        [TestMethod]
        public void SearchReservationIsNotBooked()
        {
            //arrange
            ReservationSqlDAO reservationDAO = new ReservationSqlDAO(connectionString);
            DateTime selectedFromDate = Convert.ToDateTime("2020-01-01");
            DateTime selectedToDate = Convert.ToDateTime("2020-01-10");
            int selectedCampgroundID = 4;

            //act
            string actualResult = reservationDAO.SearchReservation(selectedCampgroundID, selectedFromDate, selectedToDate);

            //assert
            Assert.AreEqual(actualResult, "Results Matching Your Search Criteria");
        }
        //[TestMethod]
        //public void MakeReservation()
        //{
        //    //arrange
        //    ReservationSqlDAO reservationDAO = new ReservationSqlDAO(connectionString);
        //    DateTime selectedFromDate = Convert.ToDateTime("2020-01-01");
        //    DateTime selectedToDate = Convert.ToDateTime("2020-01-10");

        //    //act
        //    int actualResult = reservationDAO.MakeReservation(selectedFromDate, selectedToDate);

        //    //assert
        //    Assert.AreEqual(actualResult, 5);
        //}
    }
}
