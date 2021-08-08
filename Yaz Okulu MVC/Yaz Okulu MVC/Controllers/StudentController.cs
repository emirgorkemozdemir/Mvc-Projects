using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yaz_Okulu_MVC.Models;

namespace Yaz_Okulu_MVC.Controllers
{
    public class StudentController : Controller
    {
        YazOkuluVeritabanıEntities db = new YazOkuluVeritabanıEntities();
        public ActionResult ListStudents()
        {
            var studentList = db.ÖğrenciTablosu.ToList();
            return View(studentList);
        }


        [HttpGet]
        public ActionResult AddStudents()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudents(ÖğrenciTablosu student)
        {
            db.ÖğrenciTablosu.Add(student);
            db.SaveChanges();
            return RedirectToAction("ListStudents");
        }

        public ActionResult DeleteStudents(int id)
        {
            var student = db.ÖğrenciTablosu.Find(id);
            db.ÖğrenciTablosu.Remove(student);
            db.SaveChanges();
            return RedirectToAction("ListStudents");
        }

        [HttpGet]
        public ActionResult EditStudents(int id)
        {
            var editingStudent = db.ÖğrenciTablosu.Find(id);
            return View("EditStudents",editingStudent);
        }

        [HttpPost]
        public ActionResult EditStudents(ÖğrenciTablosu student)
        {
            var tempStudent = db.ÖğrenciTablosu.Find(student.OgrID);
            tempStudent.OgrAd = student.OgrAd;
            tempStudent.OgrSoyad = student.OgrSoyad;
            tempStudent.OgrMail = student.OgrMail;
            tempStudent.OgrSifre = student.OgrSifre;
            tempStudent.Bakiye = student.Bakiye;
            tempStudent.OgrOkulNo = student.OgrOkulNo;

            db.SaveChanges();

            return RedirectToAction("ListStudents");
        }
    }
}