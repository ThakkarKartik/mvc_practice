using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Controllers
{
    public class ClientProductController : Controller
    { 
        // GET: ClientProduct
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblProduct prod)
        {
            // Add Fields which is not taken from Form
            prod.CreatedOn = DateTime.Now;
            prod.UserID = 1;
            prod.CatID = 1;

            // Save to DB
            PracticeDBEF dc = new PracticeDBEF();
            dc.tblProducts.Add(prod);
            dc.SaveChanges();
            return View();
        }
        public ActionResult ViewProd()
        {
            PracticeDBEF dc = new PracticeDBEF();
            //var Prods = dc.tblProducts.ToList();
            //return View(Prods);
            var Prods = from ob in dc.tblProducts
                        join ob2 in dc.tblUsers
                        on ob.UserID equals ob2.UserID
                        select new UserProducts
                        {
                            user = ob2,
                            product = ob
                        };

            return View(Prods);
        }
    }
}