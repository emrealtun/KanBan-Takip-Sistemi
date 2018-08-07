using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KanBanVersion2.BusinessLayer;
using KanBanVersion2.Entities;
using KanBanVersion2.WebApp.Filters;
using KanBanVersion2.WebApp.Models;

namespace KanBanVersion2.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    
    public class ProjeController : Controller
    {
        private ProjectManager ProjectManager = new ProjectManager();
        public ActionResult Index()
        {
            return View(ProjectManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proje proje = ProjectManager.Find(x => x.Id == id.Value);
            if (proje == null)
            {
                return HttpNotFound();
            }
            return View(proje);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Proje proje)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");
            if (ModelState.IsValid)
            {
                ProjectManager.Insert(proje);
                CacheHelper.RemoveProjectFromCache();
                return RedirectToAction("Index");
            }

            return View(proje);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proje proje = ProjectManager.Find(x => x.Id == id.Value);
            if (proje == null) 
            {
                return HttpNotFound();
            }
            return View(proje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proje proje)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");

            if (ModelState.IsValid)
            {
                 Proje pro = ProjectManager.Find(x => x.Id == proje.Id);
                pro.projeAd = proje.projeAd;
                pro.projeAciklamasi = proje.projeAciklamasi;
                ProjectManager.Update(pro);
                CacheHelper.RemoveProjectFromCache();


                return RedirectToAction("Index");
            }
            return View(proje);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proje proje = ProjectManager.Find(x => x.Id == id.Value);

            if (proje == null)
            {
                return HttpNotFound();
            }
            return View(proje);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proje proje = ProjectManager.Find(x => x.Id == id);
            ProjectManager.Delete(proje);
            CacheHelper.RemoveProjectFromCache();

            return RedirectToAction("Index");
        }
            
    }
}
