using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proje.Models;

namespace MVC_Proje.Controllers
{
    public class MüşteriController : Controller
    {
        StokTakipVeritabanıEntities entities = new StokTakipVeritabanıEntities();

        public ActionResult MüşteriListele()
        {
            var customers = entities.MUSTERITABLOSU.ToList();

            return View(customers);
        }


        [HttpGet]
        public ActionResult MüşteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MüşteriEkle(MUSTERITABLOSU tempMüşteri)
        {
            entities.MUSTERITABLOSU.Add(tempMüşteri);
            entities.SaveChanges();
            Response.Redirect("Listele");
            return View();
        }

        public ActionResult MüşteriSil(int id)
        {
            var customer = entities.MUSTERITABLOSU.Find(id);

            entities.MUSTERITABLOSU.Remove(customer);

            entities.SaveChanges();

            return RedirectToAction("MüşteriListele");
        }

        public ActionResult MListByID(int id)
        {
            var customer = entities.MUSTERITABLOSU.Find(id);

            return View("MListByID", customer);
        }

        public ActionResult MüşteriGüncelle(MUSTERITABLOSU tempCustomer)
        {
            var cstmr = entities.MUSTERITABLOSU.Find(tempCustomer.MUSTERIID);

            cstmr.MUSTERIAD = tempCustomer.MUSTERIAD;

            cstmr.MUSTERISOYAD = tempCustomer.MUSTERISOYAD;

            entities.SaveChanges();

            return RedirectToAction("MüşteriListele");
        }
    }
}