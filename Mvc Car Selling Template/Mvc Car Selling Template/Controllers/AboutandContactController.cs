using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Car_Selling_Template.Models;

namespace Mvc_Car_Selling_Template.Controllers
{
    public class AboutandContactController : Controller
    {
        CarSellingEntities1 db = new CarSellingEntities1();
        public ActionResult ListAboutUsAndContact()
        {
            var objects = db.AboutAndContact.Find(1);
            return View(objects);
        }

        public ActionResult AdminMainPage()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EditAboutUsAndContact(int id)
        {
          var aboutAndContact=  db.AboutAndContact.Find(id);
            return View(aboutAndContact);

        }

        [HttpPost]
        public ActionResult EditAboutUsAndContact(AboutAndContact entity)
        {

            var editingEntity = db.AboutAndContact.Find(entity.AboutID);
            editingEntity.AboutDescription = entity.AboutDescription;
            editingEntity.ContactMail = entity.ContactMail;
            editingEntity.ContactNumber = entity.ContactNumber;

            db.SaveChanges();
            return RedirectToAction("AdminMainPage");

        }
    }
}