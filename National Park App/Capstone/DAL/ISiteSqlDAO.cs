using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteSqlDAO
    {
        IList<Site> ListAllSites(int campground_id);
        string DisplaySiteInformation(int campground_id, DateTime selectedFromDate, DateTime selectedToDate);
    }
}
