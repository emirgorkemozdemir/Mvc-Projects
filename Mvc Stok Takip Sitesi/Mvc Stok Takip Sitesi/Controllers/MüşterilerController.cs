using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok_Takip_Sitesi.Models.Entity;
using PagedList;

namespace Mvc_Stok_Takip_Sitesi.Controllers
{
    public class MüşterilerController : Controller
    {
        StokTakipVeritabanıEntities1 ent = new StokTakipVeritabanıEntities1();
        public ActionResult MüşteriListele(int mstr=1)
        {
            var müşteriler = ent.MUSTERITABLOSU.ToList().ToPagedList(mstr,3);
            return View(müşteriler);
        }

        [HttpGet]

        public ActionResult MüşteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MüşteriEkle(MUSTERITABLOSU p)
        {
            ent.MUSTERITABLOSU.Add(p);
            ent.SaveChanges();
            Response.Redirect("/Müşteriler/MüşteriListele");
            return View();
        }

        public ActionResult MüşteriSil(int id)
        {
            var mstr = ent.MUSTERITABLOSU.Find(id);
            ent.MUSTERITABLOSU.Remove(mstr);
            ent.SaveChanges();
            return RedirectToAction("MüşteriListele");
        }

        public ActionResult MüşteriGüncelleAç(int id)
        {
            var mstr = ent.MUSTERITABLOSU.Find(id);

            return View("MüşteriGüncelleAç", mstr);
        }

        public ActionResult MüşteriGüncelle(MUSTERITABLOSU p)
        {
            var mstr = ent.MUSTERITABLOSU.Find(p.MUSTERIID);
            mstr.MUSTERIAD = p.MUSTERIAD;
            mstr.MUSTERISOYAD = p.MUSTERISOYAD;
            ent.SaveChanges();
            return RedirectToAction("MüşteriListele");
        }
    }
}