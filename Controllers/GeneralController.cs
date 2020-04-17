
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paytm;
using WebPractice.Models;
namespace WebPractice.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DateCompare()
        {
            return View();
        }
        public ActionResult Paytm()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Paytm(FormCollection form)
        {
            String OrderID = Services.UniqueCode();
            String merchantKey = Key.merchantKey;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Key.merchantId);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", form["txtEmail"]);
            parameters.Add("MOBILE_NO", form["txtContactNo"]);
            parameters.Add("CUST_ID", "1");
            parameters.Add("ORDER_ID", OrderID);
            parameters.Add("TXN_AMOUNT", form["txtAmount"]);
            parameters.Add("CALLBACK_URL", "http://localhost/WebPractice/General/PaytmResponse"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);
            
            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + OrderID;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;

            return View("PaymentPage");

        }

        [HttpPost]
        public ActionResult PaytmResponse(PaytmResponse data)
        {
            return View("PaytmResponse", data);
        }
    }
}