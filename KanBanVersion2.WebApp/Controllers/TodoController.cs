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
    public class TodoController : Controller
    {
        TodoManager todoManager = new TodoManager();
        ProjectManager projectManager = new ProjectManager();
        TodoKullanicisi todoKullanicisi = new TodoKullanicisi();
        KullaniciManager kullaniciManager = new KullaniciManager();

        public ActionResult Index()
        {
            var todo = todoManager.ListQueryable().Include(t => t.proje);
            return View(todo.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Todo todo = todoManager.Find( x=> x.Id == id.Value);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        public ActionResult Create()
        {
            ViewBag.projeId = new SelectList(CacheHelper.GetProjectFromCache(), "Id", "projeAd");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Todo todo)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");
            if (ModelState.IsValid)
            {//owner not yönet3
                todoManager.Insert(todo);

                return RedirectToAction("Index");
            }
            ViewBag.projeId = new SelectList(CacheHelper.GetProjectFromCache(), "Id", "projeAd", todo.projeId);
            return View(todo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = todoManager.Find(x => x.Id == id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            ViewBag.projeId = new SelectList(CacheHelper.GetProjectFromCache(), "Id", "projeAd", todo.projeId);
            ViewBag.todoKullanicisi = new SelectList(kullaniciManager.List(), "Id", "kullaniciadi", todo.todoKullanicisi);
            return View(todo);
        }

        public ActionResult GetTodoDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Todo todo = todoManager.Find(x => x.Id == id.Value);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialTodoDetails", todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Todo todo)
        {
            ModelState.Remove("duzenleyen");
            ModelState.Remove("olusturmaTarihi");
            ModelState.Remove("guncellemeTarihi");
            if (ModelState.IsValid)
            {
                Todo db_todo = todoManager.Find(x => x.Id == todo.Id);
                db_todo.taslakDurum = todo.taslakDurum;
                db_todo.todoAdi = todo.todoAdi;
                db_todo.todoAciklama = todo.todoAciklama;
                db_todo.todoDurumu = todo.todoDurumu;
                db_todo.todoKullanicisi = todo.todoKullanicisi;

                todoManager.Update(db_todo);


                return RedirectToAction("Index");
            }
            ViewBag.projeId = new SelectList(CacheHelper.GetProjectFromCache(), "Id", "projeAd", todo.projeId);
            return View(todo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = todoManager.Find(x => x.Id == id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Todo todo = todoManager.Find(x => x.Id == id);
            todoManager.Delete(todo);
            return RedirectToAction("Index");
        }

        
    }
}
