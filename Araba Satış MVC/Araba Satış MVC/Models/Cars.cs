//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Araba_Satış_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cars
    {
        public int CarID { get; set; }
        public int CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarFuelType { get; set; }
        public string CarDescription { get; set; }
        public string CarContact { get; set; }
        public string CarSeller { get; set; }
        public bool Confirmation { get; set; }
    
        public virtual Brands Brands { get; set; }
        public virtual Users Users { get; set; }
    }
}
