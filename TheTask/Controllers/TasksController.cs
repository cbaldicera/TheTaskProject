using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheTask.Models;

namespace TheTask.Controllers
{
    public class TasksController : Controller
    {
        private TaskDbContext db = new TaskDbContext();

        // Página com a lista de atividade
        public ActionResult List()
        {
            return View(db.Tasks.OrderBy(x => x.ExecutionDate).Take(5).ToList());
        }

        // Página com a lista de atividade após fazer uma busca
        [HttpPost, ActionName("List")]
        public ActionResult ListSearch(string searchParam)
        {
            return View(db.Tasks.Where(x => x.Title.Contains(searchParam)).OrderBy(x => x.ExecutionDate).Take(5).ToList());
        }

        // Página de criação de atividade
        public ActionResult Create()
        {
            return View();
        }

        // Página de criação de atividade após clicar em 'Criar'
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,ExecutionDate")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(task);
        }

        // Página de alteração de atividade
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // Página de alteração de atividade após clicar em 'Salvar'
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,ExecutionDate")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(task);
        }

        // Página de confirmação de exclusão
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // Página de confirmação de exclusão após confirmar exclusão
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
