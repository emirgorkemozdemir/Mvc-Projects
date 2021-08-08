using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Deneme_ArabaSitesi.Models;

namespace Mvc_Deneme_ArabaSitesi.Controllers
{
    public class AdminController : Controller
    {
        ArabaYoutubeEntities1 db = new ArabaYoutubeEntities1();

        public ActionResult AdminMainPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminApproveCars()
        {
            var cars = db.TableCar.Where(i => i.CarConfirmation == false).ToList();
            return View(cars);
        }


        public ActionResult AdminApproveCar(int id)
        {
            var selectedCar = db.TableCar.Where(i => i.CarID == id).SingleOrDefault();
            selectedCar.CarConfirmation = true;
            db.SaveChanges();
            Response.Write("<script>alert('Approved the car');</script>");
            return View("AdminMainPage");
        }

        public ActionResult AdminSeeMessages()
        {
            var messagesList = db.TableContact.ToList();
            return View(messagesList);
        }

        public ActionResult AdminDeleteMessage(int id)
        {
            var selectedMessage = db.TableContact.Find(id);
            db.TableContact.Remove(selectedMessage);
            db.SaveChanges();
            Response.Write("<script>alert('Message deleted !');</script>");
            return RedirectToAction("AdminSeeMessages");
        }

        public ActionResult AdminListBrands()
        {
            var brands = db.TableBrand.ToList();
            return View(brands);
        }

        [HttpGet]
        public ActionResult AdminAddBrands()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddBrands(TableBrand addingBrand)
        {
            if (ModelState.IsValid)
            {
                db.TableBrand.Add(addingBrand);
                db.SaveChanges();
                Response.Write("<script>alert('Brand added !');</script>");
                return RedirectToAction("AdminListBrands");
            }
            else
            {
                Response.Write("<script>alert('An error occured while adding Brand  !');</script>");
                return View();
            }

           
        }


        public ActionResult AdminDeleteBrand(int id)
        {
            var selectedBrand = db.TableBrand.Find(id);
            db.TableBrand.Remove(selectedBrand);
            db.SaveChanges();
            return RedirectToAction("AdminListBrands");
        }

        [HttpGet]
        public ActionResult AdminAddCar()
        {
            List<SelectListItem> brands = (from i in db.TableBrand.ToList() select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.lstBrands = brands;
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddCar(TableCar addingCar)
        {
            List<SelectListItem> brands = (from i in db.TableBrand.ToList() select new SelectListItem { Text = i.BrandName, Value = i.BrandID.ToString() }).ToList();
            ViewBag.lstBrands = brands;

         
                db.TableCar.Add(addingCar);
                db.SaveChanges();
   

            return View();
        }


        public ActionResult AdminDeleteCar(int id)
        {
            var selectedCar = db.TableCar.Find(id);
            db.TableCar.Remove(selectedCar);
            db.SaveChanges();
            return RedirectToAction("AdminApproveCars");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("ListAllCars", "Cars");
        }
    }
}