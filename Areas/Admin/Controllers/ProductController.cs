using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPractice.Models;
namespace WebPractice.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        PracticeDBEF dc = new PracticeDBEF();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            tblProduct product = dc.tblProducts.SingleOrDefault(ob => ob.ProdID == id);
            ViewBag.Product_Images = from ob in dc.tblProdImages where ob.ProdID == id select ob;
            return View(product);
        }
    }
}