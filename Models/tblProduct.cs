//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPractice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProduct
    {
        public int ProdID { get; set; }
        public string ProdName { get; set; }
        public Nullable<int> CatID { get; set; }
        public string ProdDesc { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
