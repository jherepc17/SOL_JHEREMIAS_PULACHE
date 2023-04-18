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
    public class TipoPrestamosController : Controller
    {
        private AppDatabaseContext db = new AppDatabaseContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.TiposPrestamo.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = await db.TiposPrestamo.FindAsync(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTipoPrestamo,Nombre")] TipoPrestamo tipoPrestamo)
        {
            if (ModelState.IsValid)
            {
                db.TiposPrestamo.Add(tipoPrestamo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPrestamo);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = await db.TiposPrestamo.FindAsync(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTipoPrestamo,Nombre")] TipoPrestamo tipoPrestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPrestamo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoPrestamo);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = await db.TiposPrestamo.FindAsync(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoPrestamo tipoPrestamo = await db.TiposPrestamo.FindAsync(id);
            db.TiposPrestamo.Remove(tipoPrestamo);
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
