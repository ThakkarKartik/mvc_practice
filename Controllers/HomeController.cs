using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;

namespace WebPractice.Controllers
{
    public class HomeController : Controller
    {
        readonly PracticeDBEntities db = new PracticeDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string txtEmail = form["txtEmail"];
            string txtPass = form["txtPassword"];

            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == txtEmail || ob.Password == txtPass);
            if (user != null)
                return RedirectToAction("UserProfile",user);
            else
                return View();
        }
        public ActionResult UserProfile(tblUser user)
        {
            return View(user);
        }
        [HttpPost]
        public String AndroidLogin(string txtEmail, string txtPass)
        {
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == txtEmail || ob.Password == txtPass);
            if (user != null)
                return "True";// txtEmail + " - " + txtPass; Json("True", JsonRequestBehavior.AllowGet);
            else
                return "False";//Json("False", JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}