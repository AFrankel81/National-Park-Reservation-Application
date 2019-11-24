using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int Site_Id { get; set; }
        public int Campground_Id { get; set; }
        public int Site_Number { get; set; }
        public int Max_Occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_Rv_Length { get; set; }
        public bool Utilites { get; set; }
        public string RName { get; set; }
        public decimal SDaily_Fee { get; set; }
    }
}
