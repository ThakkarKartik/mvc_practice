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
            var TitleList = from ob in dc.tblCMS select ob.Title;
            ViewBag.Titles = TitleList;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(string BlogContent, string txtTitle)
        {
            tblCM cms = new tblCM();
            cms.Title = txtTitle;
            cms.Content = BlogContent;
            cms.CreatedOn = DateTime.Now;
            cms.IsActive = true;
            dc.tblCMS.Add(cms);
            dc.SaveChanges();
            ViewBag.Content = BlogContent;
            return RedirectToAction("ViewCMS",new {Title = "CMS1" });
        }
        public ActionResult ViewCMS(int id=1)
        {
            var TitleList = from ob in dc.tblCMS select ob;
            ViewBag.CMS = TitleList;
            tblCM cms = dc.tblCMS.SingleOrDefault(ob => ob.CMSID == id);
            return View(cms);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ViewCMS(string BlogContent, int id)
        {
            tblCM cms = dc.tblCMS.SingleOrDefault(ob => ob.CMSID == id);
            cms.Content = BlogContent;
            dc.SaveChanges();
            string myOTP = Services.GenerateOTP();
            string UCode = Services.UniqueCode();

            return RedirectToAction("ViewCMS","CMS");
        }
    }
}