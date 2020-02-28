using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Areas.Admin.Controllers
{
    public class AdminInqController : Controller
    {
        PracticeDBEF dc = new PracticeDBEF();
        // GET: Admin/AdminInq
        public ActionResult Index()
        {
            var Inqs = from ob in dc.tblInquiries where ob.isReplied == false || ob.isReplied == null select ob;

            return View(Inqs);
        }

        public ActionResult Detail(int id)
        {
            tblInquiry Inq = dc.tblInquiries.SingleOrDefault(ob => ob.InqID == id);

            return View(Inq);
        }
        [HttpPost]
        public ActionResult Detail(FormCollection form, int id)
        {
            tblInquiry Inq = dc.tblInquiries.SingleOrDefault(ob => ob.InqID == id);
            Inq.Reply = form["txtReply"];
            Inq.ReplyBy = Convert.ToInt32(Session["LoginID"]);
            Inq.isReplied = true;
            dc.SaveChanges();

            // Email Code 
            MailMessage Msg = new MailMessage("keenconveyance@gmail.com", Inq.Email);
            Msg.Subject = "Reply of Your Inquiry";
            Msg.Body = form["txtReply"];
            Msg.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            NetworkCredential MyCredentials = new NetworkCredential("keenconveyance@gmail.com", "nkp12345");
            smtp.Credentials = MyCredentials;

            smtp.Send(Msg);

            return RedirectToAction("index") ;
        }
    }
}