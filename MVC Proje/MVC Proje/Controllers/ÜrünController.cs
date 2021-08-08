using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proje.Models;
namespace MVC_Proje.Controllers
{
    public class ÜrünController : Controller
    {
        StokTakipVeritabanıEntities entities = new StokTakipVeritabanıEntities();
        
        public ActionResult ÜrünListele()
        {
            var products = entities.URUNTABLOSU.ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult ÜrünEkle()
        {
            List<SelectListItem> kategoriler = (from i in entities.KATEGORITABLOSU.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            ViewBag.ktgr = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult ÜrünEkle(URUNTABLOSU tempUrun)
        {
            var ktgr = entities.KATEGORITABLOSU.Where(k => k.KATEGORIID == tempUrun.KATEGORITABLOSU.KATEGORIID).FirstOrDefault();

            tempUrun.KATEGORIID = ktgr.KATEGORIID;

            entities.URUNTABLOSU.Add(tempUrun);
            entities.SaveChanges();
            return RedirectToAction("ÜrünListele");
        }

        public ActionResult ÜrünSil(int id)
        {
            var product = entities.URUNTABLOSU.Find(id);

            entities.URUNTABLOSU.Remove(product);

            entities.SaveChanges();

            return RedirectToAction("ÜrünListele");
        }

        [HttpGet]
        public ActionResult PListByID(int id)
        {
            List<SelectListItem> kategoriler = (from i in entities.KATEGORITABLOSU.ToList() select new SelectListItem { Text = i.KATEGORIAD, Value = i.KATEGORIID.ToString() }).ToList();
            ViewBag.ktgr = kategoriler;
            var product = entities.URUNTABLOSU.Find(id);

            return View("PListByID", product);
        }

       public ActionResult ÜrünGüncelle(URUNTABLOSU tempProduct)
        {
            var product = entities.URUNTABLOSU.Find(tempProduct.URUNID);

            var ktgr = entities.KATEGORITABLOSU.Where(k => k.KATEGORIID == tempProduct.KATEGORIID).FirstOrDefault();

            product.KATEGORIID = ktgr.KATEGORIID;

            product.URUNAD = tempProduct.URUNAD;

            product.URUNFIYAT = tempProduct.URUNFIYAT;

            product.URUNSTOK = tempProduct.URUNSTOK;

            entities.SaveChanges();

            return RedirectToAction("ÜrünListele");
                
        }




    }
}