using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Areas.Admin.Controllers
{
    public class CMSController : Controller
    {
        // GET: Admin/CMS
        PracticeDBEF dc = new PracticeDBEF();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(string BlogContent)
        {
            tblCM cms = new tblCM();
            cms.Title = "CMS1";
            cms.Content = BlogContent;
            cms.CreatedOn = DateTime.Now;
            cms.IsActive = true;
            dc.tblCMS.Add(cms);
            dc.SaveChanges();
            ViewBag.Content = BlogContent;
            return RedirectToAction("ViewCMS",new {Title = "CMS1" });
        }
        public ActionResult ViewCMS(string Title)
        {
            tblCM cms = dc.tblCMS.SingleOrDefault(ob => ob.Title == Title);
            return View(cms);
        }


    }
}