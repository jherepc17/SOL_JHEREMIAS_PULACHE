using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOL_JHEREMIAS_PULACHE.Context;
using SOL_JHEREMIAS_PULACHE.Models;

namespace SOL_JHEREMIAS_PULACHE.Controllers
{
    public class TipoUsuariosController : Controller
    {
        private AppDatabaseContext db = new AppDatabaseContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.TipoUsuario.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = await db.TipoUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTipoUsuario,Nombre")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.TipoUsuario.Add(tipoUsuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoUsuario);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = await db.TipoUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTipoUsuario,Nombre")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoUsuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoUsuario);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUsuario tipoUsuario = await db.TipoUsuario.FindAsync(id);
            if (tipoUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tipoUsuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoUsuario tipoUsuario = await db.TipoUsuario.FindAsync(id);
            db.TipoUsuario.Remove(tipoUsuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
