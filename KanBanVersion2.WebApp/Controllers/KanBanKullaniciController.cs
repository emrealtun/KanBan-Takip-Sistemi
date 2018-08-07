using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KanBanVersion2.BusinessLayer;
using KanBanVersion2.BusinessLayer.Results;
using KanBanVersion2.Entities;
using KanBanVersion2.WebApp.Filters;

namespace KanBanVersion2.WebApp.Controllers
{
    [Auth] [AuthAdmin]
    [Exc]
    public class KanBanKullaniciController : Controller
    {
        private KullaniciManager km = new KullaniciManager();

        public ActionResult Index()
        {
            return View(km.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanBanKullanici kanBanKullanici = km.Find(x => x.Id == id.Value);
            if (kanBanKullanici == null)
            {
                return HttpNotFound();
            }
            return View(kanBanKullanici);
        }

        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( KanBanKullanici kanBanKullanici)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<KanBanKullanici> res = km.Insert(kanBanKullanici);
                if(res.Hata.Count > 0 )
                {
                    res.Hata.ForEach(x => ModelState.AddModelError("", x.Mesaj));
                    return View(kanBanKullanici);
                }
                return RedirectToAction("Index");
            }

            return View(kanBanKullanici);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanBanKullanici kanBanKullanici = km.Find(x => x.Id == id.Value);
            if (kanBanKullanici == null)
            {
                return HttpNotFound();
            }
            return View(kanBanKullanici);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KanBanKullanici kanBanKullanici)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<KanBanKullanici> res = km.Update(kanBanKullanici);
                if (res.Hata.Count > 0)
                {
                    res.Hata.ForEach(x => ModelState.AddModelError("", x.Mesaj));
                    return View(kanBanKullanici);
                }
                return RedirectToAction("Index"); 
            }
            return View(kanBanKullanici);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KanBanKullanici kanBanKullanici = km.Find(x => x.Id == id.Value);
            if (kanBanKullanici == null)
            {
                return HttpNotFound();
            }
            return View(kanBanKullanici);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KanBanKullanici kanBanKullanici = km.Find(x => x.Id == id);
            km.Delete(kanBanKullanici);

            return RedirectToAction("Index");
        }

    }
}
