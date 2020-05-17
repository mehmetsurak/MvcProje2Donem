using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProje.Models.Entity;
namespace MvcProje.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            if (Convert.ToString(Session["admin"]) == "evet")
            {
                var degerler = from d in db.TblMusteriler select d;
                if (!string.IsNullOrEmpty(p))
                {
                    degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));

                }
                return View(degerler.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            //var degerler = db.TblMusteriler.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler prm1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }

            db.TblMusteriler.Add(prm1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TblMusteriler.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult Guncelle(TblMusteriler prm1)
        {
            var musteri = db.TblMusteriler.Find(prm1.MUSTERIID);
            musteri.MUSTERIAD = prm1.MUSTERIAD;
            musteri.MUSTERISOYAD = prm1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
    
}