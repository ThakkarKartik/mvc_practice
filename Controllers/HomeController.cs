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
        public ActionResult UserList()
        {
            var userList = db.tblUsers.ToList();
            return View(userList);
        }
       
        public string AndroidLogin(string txtEmail="", string txtPass="")
        {
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == txtEmail || ob.Password == txtPass);
            if (user != null)
                return "True";// txtEmail + " - " + txtPass; 
            //return Json("True", JsonRequestBehavior.AllowGet);
            else
                return "False";//
            //return Json("False", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblUser user)
        {
            db.tblUsers.Add(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }


    }
}