using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ICampgroundSqlDAO
    {
        IList<Campground> ListAllCampgrounds(int park_id);
        string DisplayCampgroundInformation(int campground_id);
        
    }
}
