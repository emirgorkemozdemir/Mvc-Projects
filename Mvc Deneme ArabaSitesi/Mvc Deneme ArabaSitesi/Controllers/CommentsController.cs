using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Deneme_ArabaSitesi.Models;

namespace Mvc_Deneme_ArabaSitesi.Controllers
{
    public class CommentsController : Controller
    {
        ArabaYoutubeEntities1 db = new ArabaYoutubeEntities1();
        [HttpGet]
        public ActionResult AddComment()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(TableContact message)
        {
            db.TableContact.Add(message);
            db.SaveChanges();
            return RedirectToAction("ListAllCars","Cars");
        }
    }
}