using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Deneme_ArabaSitesi.Models;

namespace Mvc_Deneme_ArabaSitesi.Controllers
{
    public class BrandController : Controller
    {
        ArabaYoutubeEntities1 db = new ArabaYoutubeEntities1();
        public ActionResult ListBrands()
        {
            var listBrands = db.TableBrand.ToList();
            return View(listBrands);
        }
    }
}