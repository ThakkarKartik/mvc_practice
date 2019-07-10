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
    using System.ComponentModel.DataAnnotations;
    public partial class tblUser
    {
        
        public int UserID { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Password is Required"), 
            MaxLength(16,ErrorMessage ="Only 16 Character Allowed"), 
            MinLength(6,ErrorMessage ="Password too Small")]
        public string Password { get; set; }
        public string ProfPic { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
