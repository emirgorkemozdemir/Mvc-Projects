using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proje.Models;

namespace MVC_Proje.Controllers
{
    public class KategoriController : Controller
    {

        StokTakipVeritabanıEntities entities = new StokTakipVeritabanıEntities();

        public ActionResult KategoriListele()
        {
            var categories = entities.KATEGORITABLOSU.ToList();

            return View(categories);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(KATEGORITABLOSU tempKateg)
        {
            entities.KATEGORITABLOSU.Add(tempKateg);
            entities.SaveChanges();
            Response.Redirect("Listele");
            return View();
        }

        public ActionResult KategoriDelete(int id)
        {
            var ktgr = entities.KATEGORITABLOSU.Find(id);

            entities.KATEGORITABLOSU.Remove(ktgr);

            entities.SaveChanges();

            return RedirectToAction("KategoriListele");
        }

        public ActionResult ListByID(int id)
        {
            var ktgr = entities.KATEGORITABLOSU.Find(id);

            return View("ListByID", ktgr);
        }

        public ActionResult Güncelle(KATEGORITABLOSU tempKtgr)
        {
            var ktgr = entities.KATEGORITABLOSU.Find(tempKtgr.KATEGORIID);

            ktgr.KATEGORIAD = tempKtgr.KATEGORIAD;

            entities.SaveChanges();

            return RedirectToAction("KategoriListele");
        }
    }
}