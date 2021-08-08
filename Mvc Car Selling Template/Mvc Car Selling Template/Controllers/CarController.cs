using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Car_Selling_Template.Models;

namespace Mvc_Car_Selling_Template.Controllers
{
    public class CarController : Controller
    {
        CarSellingEntities1 db = new CarSellingEntities1();
        public ActionResult ListAllCars()
        {
            var carsList = db.Cars.Where(m=>m.Confirmation == true).ToList();
            return View(carsList);
        }

        public ActionResult DetailedCarByID(int id)
        {
            var car = db.Cars.Find(id);
            var photo = car.CarPhoto;
            ViewBag.carphoto = photo;
            return View(car);
        }

        [HttpGet]
        public ActionResult SendCar()
        {
            var brandList = db.Brands.ToList();
            List<SelectListItem> list =(from i in brandList select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.marka = list;
            return View();
        }

        [HttpPost]
        public ActionResult SendCar(Cars car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
            return RedirectToAction("ListAllCars");
        }

        [HttpGet]
        public ActionResult ApproveCar()
        {
            var carsToApprove = db.Cars.Where(i=>i.Confirmation==false).ToList();

            return View(carsToApprove);
        }

        public ActionResult DeleteCarApprove(int id)
        {
            var deletingCar = db.Cars.Find(id);
            db.Cars.Remove(deletingCar);
            db.SaveChanges();

            return RedirectToAction("ListAllCars");
        }

        public ActionResult SubmitCarApprove(int id)
        {
            var editingCar = db.Cars.Find(id);
            editingCar.Confirmation = true;
            db.SaveChanges();

            return RedirectToAction("ListAllCars");
        }

        public ActionResult ListCarsAdmin()
        {
            var listCars = db.Cars.Where(i=>i.Confirmation==true).ToList();
            return View(listCars);


        }

        public ActionResult DeleteCarAdmin(int id)
        {
            var deletingCar = db.Cars.Find(id);
            db.Cars.Remove(deletingCar);
            db.SaveChanges();
            return RedirectToAction("ListCarsAdmin");
        }

        [HttpGet]
        public ActionResult EditCarAdmin(int id)
        {
            var brandList = db.Brands.ToList();
            List<SelectListItem> list = (from i in brandList select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.marka = list;

            var car = db.Cars.Find(id);
            return View(car);
        }

        [HttpPost]
        public ActionResult EditCarAdmin(Cars car)
        {
            var editingCar = db.Cars.Find(car.CarID);
            editingCar.CarBrandID = car.CarBrandID;
            editingCar.CarContact = car.CarContact;
            editingCar.CarDescription = car.CarDescription;
            editingCar.CarFuelType = car.CarFuelType;
            editingCar.CarModel = car.CarModel;
            editingCar.CarPhoto = car.CarPhoto;
            editingCar.CarPrice = car.CarPrice;
            editingCar.CarSeller = car.CarSeller;

            db.SaveChanges();

            return RedirectToAction("ListCarsAdmin");
        }
    }
}