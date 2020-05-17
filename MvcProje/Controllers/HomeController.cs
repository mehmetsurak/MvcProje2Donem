using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.İsim = "Ahmet";
            //ViewData["ad"] = "Mehmet";
         
            return View();
        }

        public ActionResult About()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Giris(string id, string sifre)
        {
            if(id == "mehmet" && sifre == "surak")
            {
                Session.Add("admin", "evet");
                return RedirectToAction("Index", "Urun", new { area = "" });

            }
            else
            {
                Session.Add("admin", "hayir");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

        }

     
    }
}