using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebPractice.Models;

namespace WebPractice.Controllers
{
    public class HomeController : Controller
    {
        readonly PracticeDBEF db = new PracticeDBEF();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string txtForgetEmail)
        {
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == txtForgetEmail);

            if (user != null)
            {
                Session["LoginUserID"] = user.UserID;

                  MailMessage Msg = new MailMessage("keenconveyance@gmail.com", txtForgetEmail);
                   Msg.Subject = "Password Recovery";
                   Msg.Body = "Your OTP is : <h3>" + 1234 + "</h3>";
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
            
                Session["OTP"] = "1234";
                return View();
            }
            else
            {
                ViewBag.Error = "Account Not Found";
                return View();
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Active(int id)
        {
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.UserID == id);
            if (user.IsActive == true)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            db.SaveChanges();
            return Json(user.IsActive,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.UserID == id);
            db.tblUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string txtEmail = form["txtEmail"];
            string txtPass = form["txtPassword"];

            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.Email == txtEmail || ob.Password == txtPass);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                return RedirectToAction("UserProfile", user);
            }
            else
                return View();
        }
        public ActionResult UserProfile(tblUser user)
        {
            if (user.UserID != 0)
            {
                ViewBag.Year = user.CreatedOn.Value.Year.ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult UserList()
        {
            var userList = db.tblUsers.ToList();
            return View(userList);
        }
        public string AndroidLogin(string txtEmail = "", string txtPass = "")
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
        public ActionResult Create(FormCollection form)
        {
            try
            {
                tblUser user = new tblUser();
                user.Name = form["Name"];
                user.Password = form["Password"];
                user.Email = form["Email"];
                user.ContactNo = form["ContactNo"];
                user.ProfPic = "user2.png";
                user.IsActive = true;
                user.CreatedOn = DateTime.Now;
                db.tblUsers.Add(user);
                db.SaveChanges();

                // Auto Login after Registration
                user = db.tblUsers
                    .OrderByDescending(x => x.UserID)
                    .FirstOrDefault();
                Session["UserID"] = user.UserID;
                return RedirectToAction("UserProfile", user);

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        Response.Write(message);
                        // Display Error on Screen if any
                    }
                }
                return View();
            }


        }
        [HttpPost]
        public JsonResult IsEmailExist(string Email)
        {
            var User = (from ob in db.tblUsers where ob.Email == Email select ob);
            if (User.ToList().Count == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase profimg)
        {
            int ID = Convert.ToInt32(Session["UserID"]);
            tblUser user = db.tblUsers.SingleOrDefault(ob => ob.UserID == ID);
            user.ProfPic = profimg.FileName;
            db.SaveChanges();
            //string newImage = profimg.FileName;
            string path = Server.MapPath("~/images/");
            profimg.SaveAs(path + profimg.FileName);
            return View();
        }
    }
}