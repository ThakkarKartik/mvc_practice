using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        PracticeDBEF dc = new PracticeDBEF();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            ViewBag.UserCount = (from ob in dc.tblUsers select ob).ToList().Count.ToString();
            ViewBag.ActiveUserCount = (from ob in dc.tblUsers where ob.IsActive == true select ob).ToList().Count().ToString();
            ViewBag.ProdCount = (from ob in dc.tblProducts select ob).ToList().Count().ToString();
            ViewBag.CatCount  = (from ob in dc.tblCategories select ob).ToList().Count().ToString();

            
            return View();
        }
        public ActionResult SampleChart()
        {
            ViewBag.Y = new List<int>() { 10, 24, 23, 47, 50, 36, 27, 18 };
            ViewBag.X = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };

            //var Users = from ob in dc.tblUsers where ob.IsActive == true select ob;
            //string[] X = new string[Users.ToList().Count];
            //int[] Y = new int[Users.ToList().Count];
            //int i = 0;
            //foreach (tblUser u in Users)
            //{
            //    X[i] = u.UserID.ToString();
            //    Y[i] = (from ob in dc.tblProducts where ob.UserID == u.UserID select ob).ToList().Count;
            //    i++;
            //}
            //ViewBag.X = X;
            //ViewBag.Y = Y;

            return View();
        }
        public ActionResult List()
        {
            var User = dc.tblUsers.ToList();
            return View(User);
        }
        public ActionResult Details(int id)
        {
            tblUser user = dc.tblUsers.SingleOrDefault(ob => ob.UserID == id);
            var prods =  (from ob in dc.tblProducts where ob.UserID == user.UserID select ob).ToList();
            ViewBag.Products = prods;   // as IEnumerable<tblProduct>;
            return View(user);
        }
        public ActionResult AddNew()
        {
            var country = dc.CountryMasters;
            ViewBag.Country = new SelectList(country, "ID", "Name");
            return View();
        }
        [HttpPost]
        public JsonResult getState(int cid)
        {
            var State = from ob in dc.StateMasters where ob.CountryID == cid select ob;
            SelectList statelist = new SelectList(State, "ID", "Name");
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCity(int sid)
        {
            var City = from ob in dc.CityMasters where ob.StateID == sid select ob;
            SelectList citylist = new SelectList(City, "ID", "Name");
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }
    }
}