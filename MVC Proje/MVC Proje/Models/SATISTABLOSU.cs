//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Proje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SATISTABLOSU
    {
        public int SATISID { get; set; }
        public Nullable<int> URUNID { get; set; }
        public Nullable<int> MUSTERIID { get; set; }
        public Nullable<int> SATISADET { get; set; }
        public Nullable<int> SATISFIYAT { get; set; }
    
        public virtual MUSTERITABLOSU MUSTERITABLOSU { get; set; }
        public virtual URUNTABLOSU URUNTABLOSU { get; set; }
    }
}
