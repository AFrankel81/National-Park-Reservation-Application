using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationSqlDAO
    {
        string SearchReservation(int selectedCampgroundID, DateTime selectedFromDate, DateTime selectedToDate);
        List<Site> FindOpenSites(int selectedCampgroundID, DateTime selectedFromDate, DateTime selectedToDate);
        int MakeReservation(DateTime reserveFromDate, DateTime reserveToDate);
        bool MakeSureCampgroundIsOpen(DateTime selectedFromDate, DateTime selectedToDate, int selectedCampgroundID);
    }
}
