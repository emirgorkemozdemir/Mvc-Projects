using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok_Takip_Sitesi.Models.Entity;
using PagedList;

namespace Mvc_Stok_Takip_Sitesi.Controllers
{
    public class KategoriController : Controller
    {
        StokTakipVeritabanıEntities1 ent = new StokTakipVeritabanıEntities1();
        public ActionResult KategoriListele(string ps,int kategori=1)
        {
            var kategoriler = from d in ent.KATEGORITABLOSU select d;
            if (!string.IsNullOrEmpty(ps))
            {
                kategoriler = kategoriler.Where(m => m.KATEGORIAD.Contains(ps));
            }
            return View(kategoriler.ToList().ToPagedList(kategori, 3));
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(KATEGORITABLOSU p)
        {
            ent.KATEGORITABLOSU.Add(p);
            ent.SaveChanges();
            Response.Redirect("/Kategori/KategoriListele");
            return View();
            
        }

        public ActionResult KategoriSil(int id)
        {
            var ktgr = ent.KATEGORITABLOSU.Find(id);
            ent.KATEGORITABLOSU.Remove(ktgr);

            ent.SaveChanges();
            return RedirectToAction("KategoriListele");
        }

        public ActionResult KategoriGüncelleAç(int id)
        {
            var ktgr = ent.KATEGORITABLOSU.Find(id);

            return View("KategoriGüncelleAç", ktgr);
        }

        public ActionResult KategoriGüncelle(KATEGORITABLOSU p1)
        {
            var ktgr = ent.KATEGORITABLOSU.Find(p1.KATEGORIID);
            ktgr.KATEGORIAD = p1.KATEGORIAD;
            ent.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
    }
}