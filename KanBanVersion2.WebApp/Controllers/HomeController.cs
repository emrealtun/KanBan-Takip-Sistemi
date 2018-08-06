
using KanBanVersion2.BusinessLayer;
using KanBanVersion2.BusinessLayer.Results;
using KanBanVersion2.Entities;
using KanBanVersion2.Entities.Mesajlar;
using KanBanVersion2.Entities.ValueObjects;
using KanBanVersion2.WebApp.Models;
using KanBanVersion2.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KanBanVersion2.WebApp.Controllers
{
    public class HomeController : Controller
    {
        TodoManager tm = new TodoManager();
        ProjectManager pm = new ProjectManager();
        KullaniciManager km = new KullaniciManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Todo()
        {
            return View(tm.ListQueryable().Where(x=> x.taslakDurum==false && x.todoDurumu == todoDurum.TODO).OrderByDescending(x=> x.guncellemeTarihi).ToList());
        }
        public ActionResult Analyze()
        {
            return View(tm.ListQueryable().Where(x => x.taslakDurum == false && x.todoDurumu == todoDurum.ANALYZE).OrderByDescending(x => x.guncellemeTarihi).ToList());
        }
        public ActionResult Develop()
        {
            return View(tm.ListQueryable().Where(x => x.taslakDurum == false && x.todoDurumu == todoDurum.DEVELOP).OrderByDescending(x => x.guncellemeTarihi).ToList());
        }
        public ActionResult Test()
        {
            return View(tm.ListQueryable().Where(x => x.taslakDurum == false && x.todoDurumu == todoDurum.TEST).OrderByDescending(x => x.guncellemeTarihi).ToList());
        }
        public ActionResult Done()
        {
            return View(tm.ListQueryable().Where(x => x.taslakDurum == false && x.todoDurumu == todoDurum.DONE).OrderByDescending(x => x.guncellemeTarihi).ToList());
        }

        public ActionResult ByProje(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              
            Proje p = pm.Find(x=> x.Id== id.Value);
            if (p == null)
            {
                return HttpNotFound();
            }
                
            return View("Todo", p.Todo.Where(x => x.taslakDurum == false).OrderByDescending(x=> x.guncellemeTarihi).ToList());
         }

        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
           
            BusinessLayerResult<KanBanKullanici> res = km.LoginUser(model);

            if (res.Hata.Count > 0)
            {
                if(res.Hata.Find(x => x.Kod == HataMesajiKodlari.KullaniciAktifDegil)  != null)
                {
                    ViewBag.SetLink = "http://Home/Login/134-5678-56546";
                }
                res.Hata.ForEach(x => ModelState.AddModelError("", x.Mesaj));
                return View(model);
            }
            CurrentSession.Set<KanBanKullanici>("login",res.Result);//session kullanıcı bilgi saklama
           return RedirectToAction("Todo");
        }

        public ActionResult Register( )
                {

                    return View();
                }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                   BusinessLayerResult<KanBanKullanici> res =km.RegisterUser(model);

                if(res.Hata.Count > 0)
                {
                    res.Hata.ForEach(x => ModelState.AddModelError("", x.Mesaj));
                    return View(model);
                }

                OkViewModel ok = new OkViewModel()
                {
                    Title = "Kayıt Başarılı...Giriş Sayfasına Yönlendiriliyorsunuz",
                    RedirectingUrl = "/Home/Login",
                    RedirectingTimeOut = 8000,
                };
                
                ok.Items.Add("Lütfen e-posta adresinize gelen aktivasyon linkine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden giriş yapamazsınız");

                return View("Ok", ok);
            }
            return View(model);
        }
 
        public ActionResult KullaniciAktivasyon(Guid id)
        {
            BusinessLayerResult<KanBanKullanici> res = km.Activateuser(id);
            if (res.Hata.Count>0)
            {
                ErrorViewModel evm = new ErrorViewModel()
                {
                    Title = "Geçersiz İşem",
                    RedirectingUrl = "/Home/Login",
                    RedirectingTimeOut = 8000,
                    Items = res.Hata
                 };
            
                return View("Error",evm);
            }
            OkViewModel ok = new OkViewModel()
            {
                Title = "Aktivasyon Başarılı...Giriş Sayfasına Yönlendiriliyorsunuz",
                RedirectingUrl = "/Home/Login",
                RedirectingTimeOut = 8000,
            };

            ok.Items.Add("Hesabınız aktif edilmiştir. Giriş yapabilirsiniz");

            return View("Ok",ok);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
     }
 }

    