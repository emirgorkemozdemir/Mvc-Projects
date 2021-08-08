using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Araba_Satış_MVC.Models;

namespace Araba_Satış_MVC.Controllers
{
    public class NonUserController : Controller
    {

        CarsDBEntities db = new CarsDBEntities();

        public ActionResult MainPageNonUsers()
        {
            return View();
        }

        public ActionResult ShowBrandsNonUser()
        {
            var myBrandsList = db.Brands.ToList();
            return View(myBrandsList);
        }
    }
}