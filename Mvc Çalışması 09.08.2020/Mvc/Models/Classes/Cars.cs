using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models.Classes
{
    public class Cars
    {
        [Key]
        public int CarID { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarFuelType { get; set; }
        public string CarDescription { get; set; }
        public string CarContact { get; set; }
        public int CarSeller { get; set; }
    }
}