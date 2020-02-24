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