using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yemek_Sitesi_MVC.Models;
using Yemek_Sitesi_MVC.Models.Classes;

namespace Yemek_Sitesi_MVC.Controllers
{
    public class AdminController : Controller
    {
        Yemek_Sitesi_VeritabanıEntities1 db = new Yemek_Sitesi_VeritabanıEntities1();
        public ActionResult AdminListRecipes()
        {
            var listRecipes = db.YemekTablosu.ToList();
            return View(listRecipes);
        }

        [HttpGet]
        public ActionResult UserSettingsForAdmin()
        {
            var selectedUser = GlobalVariables.loggedUser;
            return View(selectedUser);
        }

        [HttpPost]
        public ActionResult UserSettingsForAdmin(YöneticiTablosu tempUser)
        {

            if (Sha265Converter.ComputeSha256Hash(tempUser.oldPassword) == GlobalVariables.loggedUser.Şifre)
            {
                var editingUser = db.YöneticiTablosu.Where(i => i.KullanıcıAdı == GlobalVariables.loggedUser.KullanıcıAdı).SingleOrDefault();
                editingUser.Şifre = Sha265Converter.ComputeSha256Hash(tempUser.NewPassword);
                db.SaveChanges();
                return RedirectToAction("MainPageForUsers", "Recipe");
            }
            else
            {
                Response.Write("Eski şifre yanlış !");
                return RedirectToAction("UserSettings");
            }
        }

        public ActionResult AdminDeleteRecipe(int id)
        {
            var deletingRecipe = db.YemekTablosu.Find(id);
            db.YemekTablosu.Remove(deletingRecipe);
            db.SaveChanges();
            return RedirectToAction("AdminListRecipes");
        }

        [HttpGet]
        public ActionResult AdminAddRecipe()
        {
            var list = db.KategoriTablosu.ToList();
            ViewBag.clist = list;
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddRecipe(YemekTablosu newRecipe)
        {
            db.YemekTablosu.Add(newRecipe);
            newRecipe.EklenmeTarihi = DateTime.Now.Date;
            db.SaveChanges();
            return RedirectToAction("AdminListRecipes");
        }

        [HttpGet]
        public ActionResult AdminEditRecipe(int id)
        {
            
            var list = db.KategoriTablosu.ToList();
            ViewBag.clist = list;
            var selectedRecipe = db.YemekTablosu.Find(id);
            return View(selectedRecipe);
        }

        [HttpPost]
        public ActionResult AdminEditRecipe(YemekTablosu editingRecipe)
        {
            var selectedRecipe = db.YemekTablosu.Find(editingRecipe.YemekID);
            selectedRecipe.YemekAd = editingRecipe.YemekAd;
            selectedRecipe.KategoriID = editingRecipe.KategoriID;
            selectedRecipe.Malzemeler = editingRecipe.Malzemeler;
            selectedRecipe.Resimler = editingRecipe.Resimler;
            selectedRecipe.Tarifler = editingRecipe.Tarifler;
            selectedRecipe.EklenmeTarihi = DateTime.Now.Date;

            db.SaveChanges();
            return RedirectToAction("AdminListRecipes");
        }


        public ActionResult AdminApproveRecipesFromUsers()
        {
            var listRecipes = db.TarifVermeTablosu.ToList();
            return View(listRecipes);
        }

        public ActionResult AdminConfirmRecipe(int id)
        {
            var selectedRecipe = db.TarifVermeTablosu.Find(id);
            selectedRecipe.TarifOnay = true;
            db.SaveChanges();
            return RedirectToAction("AdminApproveRecipesFromUsers");
        }

        public ActionResult AdminUnconfirmRecipe(int id)
        {
            var selectedRecipe = db.TarifVermeTablosu.Find(id);
            selectedRecipe.TarifOnay = false;
            db.SaveChanges();
            return RedirectToAction("AdminApproveRecipesFromUsers");
        }
        public ActionResult AdminDeleteRecipeFromUser(int id)
        {
            var selectedUser = db.TarifVermeTablosu.Find(id);
            db.TarifVermeTablosu.Remove(selectedUser);
            db.SaveChanges();
            return RedirectToAction("AdminApproveRecipesFromUsers");
        }


        [HttpGet]
        public ActionResult AdminAddTodaysRecipe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAddTodaysRecipe(GününYemeğiTablosu tdRecipe)
        {
            tdRecipe.GYemekTarih = DateTime.Now.Date;
            db.GününYemeğiTablosu.Add(tdRecipe);
            db.SaveChanges();
            return RedirectToAction("AdminListRecipes");
        }

        [HttpGet]
        public ActionResult AdminEditAboutUs()
        {
            var selectedAbout = db.HakkımızdaTablosu.Find(1);
            return View(selectedAbout);
        }

        [HttpPost]
        public ActionResult AdminEditAboutUs(HakkımızdaTablosu postedAbout)
        {
            var editingAbout = db.HakkımızdaTablosu.Find(postedAbout.HakkımızdaID);
            editingAbout.Hakkımızda = postedAbout.Hakkımızda;
            editingAbout.Resim = postedAbout.Resim;
            db.SaveChanges();
            return RedirectToAction("AdminListRecipes");
        }

        [HttpGet]
        public ActionResult AdminApproveComments()
        {
            var listComments = db.YorumlarTablosu.ToList();
            return View(listComments);
        }

        public ActionResult AdminApproveComment(int id)
        {
            var selectedComment = db.YorumlarTablosu.Find(id);
            selectedComment.YorumOnay = 1;
            db.SaveChanges();
            return RedirectToAction("AdminApproveComments");
        }
    }
}