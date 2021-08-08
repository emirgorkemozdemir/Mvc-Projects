using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Kütüphane.Models;

namespace Mvc_Kütüphane.Controllers
{
    public class BooksController : Controller
    {
        KütüphaneProjesiEntities db = new KütüphaneProjesiEntities();
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult ListAllBooks()
        {
            var allBooks = db.KütüphaneKitapTablosu.ToList();
            return View(allBooks);
        }

        public ActionResult GetBook(int id)
        {
            var selectedBook = db.KütüphaneKitapTablosu.Find(id);
            if (selectedBook.KitapAdeti == 0)
            {
                Response.Write("<script>alert('Sipariş ettiğiniz kitap stokta bulunmamaktadır ! Sipariş gerçekleştirilemedi !');</script>");
                return View();
            }
            else
            {
                selectedBook.KitapAdeti -= 1;

                AlınanKitapTablosu newTakenBook = new AlınanKitapTablosu();
                newTakenBook.AlınanKitapAdı = selectedBook.KitapAdı;
                newTakenBook.KitabıAlanKullanıcı = LoggedUserInfo.Mail;
                newTakenBook.KitapAlınmaTarihi = DateTime.Now;

                db.AlınanKitapTablosu.Add(newTakenBook);

                db.SaveChanges();
            }


            return RedirectToAction("ListAllBooks");
        }


        public ActionResult ShowBooksOnMe( string userinfo)
        {
            var list = db.AlınanKitapTablosu.Where(i => i.KitabıAlanKullanıcı == userinfo).ToList();
            return View(list);
        }

     
    }
}