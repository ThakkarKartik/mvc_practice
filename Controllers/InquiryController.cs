using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Controllers
{
    public class InquiryController : Controller
    {
        PracticeDBEF dc = new PracticeDBEF();
        // GET: Inquiry
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            tblInquiry Inq = new tblInquiry();
            Inq.FirstName = form["txtFname"];
            Inq.LastName = form["txtLname"];
            Inq.ContactNo = form["txtContactNo"];
            Inq.Email = form["txtEmail"];
            Inq.Subject = form["txtSubject"];
            Inq.Query = form["txtQuery"];
            Inq.CreatedOn = DateTime.Now;
            dc.tblInquiries.Add(Inq);
            dc.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}