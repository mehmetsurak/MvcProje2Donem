using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProje.Models.Entity;
namespace MvcProje.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            if(Convert.ToString(Session["admin"]) == "evet")
            {
                var degerler = db.TblUrunler.ToList();
                return View(degerler);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

        }

        // Ürün ekleme fonksiyonu
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TblUrunler prm1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KATEGORIID == prm1.TblKategoriler.KATEGORIID).FirstOrDefault();
            prm1.TblKategoriler = ktg;
            db.TblUrunler.Add(prm1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TblUrunler.Find(id);
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TblUrunler prm1)
        {
            var urun = db.TblUrunler.Find(prm1.URUNID);
            urun.URUNAD = prm1.URUNAD;
            urun.AD = prm1.AD;
            urun.FIYAT = prm1.FIYAT;
            urun.STOK = prm1.STOK;
            //urun.URUNKATEGORI = prm1.URUNKATEGORI;
            var ktg = db.TblKategoriler.Where(m => m.KATEGORIID == prm1.TblKategoriler.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}