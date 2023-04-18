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
    public class LibrosController : Controller
    {
        private AppDatabaseContext db = new AppDatabaseContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Libros.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = await db.Libros.FindAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDLibro,Nombre,Categoria,Autor,Pais,FechaPublicacion,Editorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libros.Add(libro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = await db.Libros.FindAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDLibro,Nombre,Categoria,Autor,Pais,FechaPublicacion,Editorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(libro);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = await db.Libros.FindAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Libro libro = await db.Libros.FindAsync(id);
            db.Libros.Remove(libro);
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
