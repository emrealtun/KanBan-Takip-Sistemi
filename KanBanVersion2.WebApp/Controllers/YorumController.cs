using KanBanVersion2.BusinessLayer;
using KanBanVersion2.Entities;
using KanBanVersion2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KanBanVersion2.WebApp.Controllers
{
    public class YorumController : Controller
    {
        private TodoManager todoManager = new TodoManager();
        private YorumManager yorumManager = new YorumManager();

        public ActionResult ShowTodoYorum(int? id)
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

            return PartialView("_PartialYorumlar", todo.Yorum);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if(ModelState.IsValid)
            {
                ModelState.Remove("duzenleyen");
                ModelState.Remove("olusturmaTarihi");
                ModelState.Remove("guncellemeTarihi");

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = yorumManager.Find(x => x.Id == id);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            yorum.icerik = text;
            if (yorumManager.Update(yorum) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
        }            return Json(new { result = false }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Yorum yorum = yorumManager.Find(x => x.Id == id);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            if (yorumManager.Delete(yorum) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Yorum yorum,int? todoid)
        {
            if(ModelState.IsValid)
            {
                ModelState.Remove("duzenleyen");
                ModelState.Remove("olusturmaTarihi");
                ModelState.Remove("guncellemeTarihi");

                if (todoid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Todo todo = todoManager.Find(x => x.Id == todoid);
                if (todo == null)
                {
                    return new HttpNotFoundResult();
                }
                yorum.todo = todo;
                yorum.kullanici = CurrentSession.kullanici;

                if (yorumManager.Insert(yorum) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}