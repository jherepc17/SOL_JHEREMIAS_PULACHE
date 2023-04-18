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
using static SOL_JHEREMIAS_PULACHE.Models.EnumAlert;

namespace SOL_JHEREMIAS_PULACHE.Controllers
{
    public class UsuariosController : Controller
    {
        private AppDatabaseContext db = new AppDatabaseContext();

        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.TipoUsuario);
            return View(await usuarios.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.Include(u => u.TipoUsuario).FirstAsync(a => a.IdUsuario == id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult Create()
        {
            Usuario usuario = new Usuario();

            usuario.Estado = true;
            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuario, "IdTipoUsuario", "Nombre");
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUsuario,Dni,Nombres,Apellidos,Email,Telefono,Estado,IdTipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuario, "IdTipoUsuario", "Nombre", usuario.IdTipoUsuario);
            return View(usuario);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuario, "IdTipoUsuario", "Nombre", usuario.IdTipoUsuario);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUsuario,Dni,Nombres,Apellidos,Email,Telefono,Estado,IdTipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoUsuario = new SelectList(db.TipoUsuario, "IdTipoUsuario", "Nombre", usuario.IdTipoUsuario);
            return View(usuario);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.Include(u => u.TipoUsuario).FirstAsync(a => a.IdUsuario == id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuario);
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
