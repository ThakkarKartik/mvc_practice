using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebPractice.Controllers
{
    public class UserAPIController : ApiController
    {
        public string GetName()
        {
            return "Welcome to my API";
        }
    }
}
