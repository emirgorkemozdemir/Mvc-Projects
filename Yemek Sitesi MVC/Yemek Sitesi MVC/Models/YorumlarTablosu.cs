//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yemek_Sitesi_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class YorumlarTablosu
    {
        public int YorumID { get; set; }
        public string YorumAd { get; set; }
        public string Mail { get; set; }
        public System.DateTime Tarih { get; set; }
        public int YorumOnay { get; set; }
        public string Yorumİçerik { get; set; }
        public int YemekID { get; set; }
    
        public virtual YemekTablosu YemekTablosu { get; set; }
    }
}