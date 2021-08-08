using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok_Takip_Sitesi.Models.Entity;

namespace Mvc_Stok_Takip_Sitesi.Controllers
{
    public class SatışController : Controller
    {
        // GET: Satış
        public ActionResult SatışListele()
        {
            List<SelectListItem> urunler = (from i in ent.URUNTABLOSU.ToList() select new SelectListItem { Text = i.URUNAD, Value = i.URUNID.ToString() }).ToList();
            ViewBag.sells = urunler;

            List<SelectListItem> musteri = (from i in ent.MUSTERITABLOSU.ToList() select new SelectListItem { Text = i.MUSTERIAD + " " + i.MUSTERISOYAD, Value = i.MUSTERIID.ToString() }).ToList();
            ViewBag.mstrler = musteri;
            return View();
        }

        StokTakipVeritabanıEntities1 ent = new StokTakipVeritabanıEntities1();

        [HttpGet]
        public ActionResult SatışEkle()
        {

           
            return View();

        }

       [HttpPost]
       public ActionResult SatışEkle(SATISTABLOSU p)
        {

            var urun = ent.URUNTABLOSU.Where(m => m.URUNID == p.URUNTABLOSU.URUNID).FirstOrDefault();
            var mstr = ent.MUSTERITABLOSU.Where(m => m.MUSTERIID == p.MUSTERITABLOSU.MUSTERIID).FirstOrDefault();

            if (urun.URUNSTOK<=0)
            {
                Response.Write("Stokta seçilen ürün kalmamıştır");
                return RedirectToAction("SatışListele");
            }
            else
            {
                urun.URUNSTOK = urun.URUNSTOK - 1;
                p.URUNTABLOSU = urun;
                p.MUSTERITABLOSU = mstr;
                ent.SATISTABLOSU.Add(p);
                ent.SaveChanges();
                return RedirectToAction("SatışListele");
            }

            
        }

    }
}