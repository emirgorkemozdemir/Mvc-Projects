using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yaz_Okulu_MVC.Models;

namespace Yaz_Okulu_MVC.Controllers
{
    public class TeacherController : Controller
    {
        YazOkuluVeritabanıEntities db = new YazOkuluVeritabanıEntities();
        public ActionResult ListTeachers()
        {
            var teacherList = db.ÖğretmenTablosu.ToList();
            return View(teacherList);
        }

        [HttpGet]
        public ActionResult AddTeachers()
        {
            List<SelectListItem> dersler = (from i in db.DersTablosu.ToList() select new SelectListItem { Text = i.DersAd, Value = i.DersID.ToString() }).ToList();

            ViewBag.urn = dersler;
            return View();
        }

        [HttpPost]
        public ActionResult AddTeachers(ÖğretmenTablosu teacher)
        {
            db.ÖğretmenTablosu.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("ListTeachers");
        }

        public ActionResult DeleteTeachers(int id)
        {
            var teacher = db.ÖğretmenTablosu.Find(id);
            db.ÖğretmenTablosu.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("ListTeachers");
        }

        [HttpGet]
        public ActionResult EditTeachers(int id)
        {
            var editingTeacher = db.ÖğretmenTablosu.Find(id);
            List<SelectListItem> dersler = (from i in db.DersTablosu.ToList() select new SelectListItem { Text = i.DersAd, Value = i.DersID.ToString() }).ToList();
            ViewBag.lessons = dersler;
            return View("EditTeachers",editingTeacher);
        }

        [HttpPost]
        public ActionResult EditTeachers(ÖğretmenTablosu teacher)
        {
            var tempTeacher = db.ÖğretmenTablosu.Find(teacher.OgrtID);
            tempTeacher.OgrtAdSoyad = teacher.OgrtAdSoyad;
            tempTeacher.OgrtDersID = teacher.OgrtDersID;
            db.SaveChanges();
            return RedirectToAction("ListTeachers");
        }
    }
}