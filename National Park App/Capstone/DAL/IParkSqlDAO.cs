using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IParkSqlDAO
    {
        IList<Park> ListAllParks();
        string DisplayParkInformation(int park_id);
    }
}
