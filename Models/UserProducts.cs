using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPractice.Models
{
    public class UserProducts
    {
        public tblUser user { get; set; }
        public tblProduct product { get; set; }
    }
}