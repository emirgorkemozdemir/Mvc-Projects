using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yaz_Okulu_MVC.Models;

namespace Yaz_Okulu_MVC.Controllers
{
    public class LessonController : Controller
    {
        YazOkuluVeritabanıEntities db = new YazOkuluVeritabanıEntities();
        public ActionResult ListLessons()
        {
            var lessonList = db.DersTablosu.ToList();
            return View(lessonList);
        }

        public ActionResult DeleteLessons(int id)
        {
            var lesson = db.DersTablosu.Find(id);
            db.DersTablosu.Remove(lesson);
            db.SaveChanges();

            return RedirectToAction("ListLessons");
        }

        [HttpGet]
        public ActionResult AddLessons()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLessons(DersTablosu lesson)
        {
            db.DersTablosu.Add(lesson);
            db.SaveChanges();
            return RedirectToAction("ListLessons");
        }

        [HttpGet]
        public ActionResult EditLessons(int id)
        {
            var lesson = db.DersTablosu.Find(id);
            return View("EditLessons", lesson);
        }

        [HttpPost]
        public ActionResult EditLessons(DersTablosu lesson)
        {
            var editLesson = db.DersTablosu.Find(lesson.DersID);
            editLesson.DersAd = lesson.DersAd;
            editLesson.MaxKont = lesson.MaxKont;
            editLesson.MinKont = lesson.MinKont;
            editLesson.OgrSayısı = lesson.OgrSayısı;

            db.SaveChanges();

            return RedirectToAction("ListLessons");
        }
    }
}