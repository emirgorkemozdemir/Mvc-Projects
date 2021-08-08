using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok_Takip_Sitesi.Models.Entity;
using PagedList;

namespace Mvc_Stok_Takip_Sitesi.Controllers
{
    public class ÜrünlerController : Controller
    {
        StokTakipVeritabanıEntities1 ent = new StokTakipVeritabanıEntities1();

       public ActionResult ÜrünListele(int urun=1)
        {
            var ürünler = ent.URUNTABLOSU.ToList().ToPagedList(urun,3);
            return View(ürünler);
        }


        [HttpGet]
        public ActionResult ÜrünEkle()
        {
            List<SelectListItem> ktgr = (from i in ent.KATEGORITABLOSU.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            ViewBag.Kategoriler = ktgr;
            return View();
        }

        [HttpPost]
        public ActionResult ÜrünEkle(URUNTABLOSU p)
        {
            var ktgr = ent.KATEGORITABLOSU.Where(m => m.KATEGORIID == p.KATEGORITABLOSU.KATEGORIID).FirstOrDefault();
            p.KATEGORITABLOSU =ktgr;
            ent.URUNTABLOSU.Add(p);
            ent.SaveChanges();
            return RedirectToAction("ÜrünListele");
        }

        public ActionResult ÜrünSil(int id)
        {
            var urun = ent.URUNTABLOSU.Find(id);
            ent.URUNTABLOSU.Remove(urun);
            ent.SaveChanges();
            return RedirectToAction("ÜrünListele");
        }

        public ActionResult ÜrünGüncelleAç(int id)
        {
            var urun = ent.URUNTABLOSU.Find(id);
            List<SelectListItem> ktgr = (from i in ent.KATEGORITABLOSU.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            ViewBag.Kategoriler = ktgr;
            return View("ÜrünGüncelleAç", urun);
        }

        public ActionResult ÜrünGüncelle(URUNTABLOSU p)
        {
            var urun = ent.URUNTABLOSU.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            var ktgr = ent.KATEGORITABLOSU.Where(m => m.KATEGORIID == p.KATEGORITABLOSU.KATEGORIID).FirstOrDefault();
            urun.KATEGORIID = ktgr.KATEGORIID;
            urun.URUNFIYAT = p.URUNFIYAT;
            urun.URUNSTOK = p.URUNSTOK;
            ent.SaveChanges();
            return RedirectToAction("ÜrünListele");
        }
    }
}