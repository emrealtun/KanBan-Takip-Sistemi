


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KanBanVersion2.BusinessLayer;
using KanBanVersion2.BusinessLayer.Results;
using KanBanVersion2.Entities;
using KanBanVersion2.Entities.Mesajlar;
using KanBanVersion2.Entities.ValueObjects;
using KanBanVersion2.WebApp.Filters;
using KanBanVersion2.WebApp.Models;
using KanBanVersion2.WebApp.ViewModels;


namespace KanBanVersion2.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class ProfilController : Controller
    {
        KullaniciManager km = new KullaniciManager();
        TodoManager tm = new TodoManager();
        ProjectManager pm = new ProjectManager();




        // GET: Profil
        public ActionResult Profil()
        {
            return View(tm.ListQueryable().OrderByDescending(x => x.guncellemeTarihi).ToList());
        }
        public ActionResult ByProje(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Proje p = pm.Find(x => x.Id == id.Value);
            if (p == null)
            {
                return HttpNotFound();
            }

            return View("Todo", p.Todo.OrderByDescending(x => x.guncellemeTarihi).ToList());
        }
        public ActionResult ProfilDuzenle()
        {
            BusinessLayerResult<KanBanKullanici> res = km.GetUserById(CurrentSession.kullanici.Id);

            if(res.Hata.Count>0)
            {
                ErrorViewModel hata = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Hata
                };

                return View("Error", hata);
            }
            return View(res.Result);
        }

        [HttpPost]
        public ActionResult ProfilDuzenle(KanBanKullanici kullanici)
        {
            ModelState.Remove("duzenleyen");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<KanBanKullanici> res = km.UpdateProfil(kullanici);
                if (res.Hata.Count > 0)
                {
                    ErrorViewModel error = new ErrorViewModel();

                    error.Items = res.Hata;
                    error.Title = "Profil Güncellenemedi";
                    error.RedirectingUrl = "/Profile/ProfilDuzenle";

                    return View("Error", error);
                }
                //profil güncellendiği için session güncellendi
                CurrentSession.Set<KanBanKullanici>("login",res.Result);

                return RedirectToAction("Profil");
            }
            return View(kullanici);
        }
        public ActionResult ProfilSil()
        {

            BusinessLayerResult<KanBanKullanici> res = km.RemoveUserById(CurrentSession.kullanici.Id);

            if (res.Hata.Count > 0)
            {
                ErrorViewModel mesaj = new ErrorViewModel()
                {
                    Items = res.Hata,
                    Title = "Profil Silinemedi",
                    RedirectingUrl = "/Profile/ProfizDuzenle"
                };
                return View("Error", mesaj);
            }

            Session.Clear();
            return RedirectToAction("Login","Home");
        }




    }
}