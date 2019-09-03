using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebPractice.Models;

namespace WebPractice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Xamarin" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Xamarin.svc or Xamarin.svc.cs at the Solution Explorer and start debugging.
    public class Xamarin : IXamarin
    {
        PracticeDBEF db = new PracticeDBEF();
        public string UserLogin(string uid, string pass)
        {
            //Xamarin api = new Xamarin();
            //return api.UserLogin(uid, pass);
            
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == uid || ob.Password == pass);
            if (user != null)
                return "True";// txtEmail + " - " + txtPass; 
            //return Json("True", JsonRequestBehavior.AllowGet);
            else
                return "False";//
            //return Json("False", JsonRequestBehavior.AllowGet);
            
        }
    }
}
