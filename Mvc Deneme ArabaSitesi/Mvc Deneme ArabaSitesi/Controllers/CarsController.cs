using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Deneme_ArabaSitesi.Models;

namespace Mvc_Deneme_ArabaSitesi.Controllers
{
    public class CarsController : Controller
    {
        ArabaYoutubeEntities1 db = new ArabaYoutubeEntities1();
        public ActionResult ListCarsForBrands(int id)
        {
            var selectedCars = db.TableCar.Where(i => i.CarBrandID == id && i.CarConfirmation==true).ToList();
            return View(selectedCars);
        }

        public ActionResult ListAllCars()
        {
            var selectedCarsAll = db.TableCar.Where(i => i.CarConfirmation == true).ToList();
            return View(selectedCarsAll);
        }

        public ActionResult CarsDetail(int id)
        {
            var selectedCar = db.TableCar.Where(i => i.CarID == id).SingleOrDefault();
            ViewBag.photo = selectedCar.CarPhoto;

            return View(selectedCar);
        }

        [HttpGet]
        public ActionResult AddCars()
        {
            List<SelectListItem> brands = (from i in db.TableBrand.ToList() select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.lstBrands = brands;
            return View();
        }


        [HttpPost]
        public ActionResult AddCars(TableCar addingCar)
        {
            List<SelectListItem> brands = (from i in db.TableBrand.ToList() select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.lstBrands = brands;

            if (ModelState.IsValid)
            {
                db.TableCar.Add(addingCar);
                db.SaveChanges();
            }

            return View();
         
        }

    }
}