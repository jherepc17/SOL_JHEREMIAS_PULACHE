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
using Rotativa;
using SOL_JHEREMIAS_PULACHE.Models.ViewModel;

namespace SOL_JHEREMIAS_PULACHE.Controllers
{
    public class PrestamosController : Controller
    {
        private AppDatabaseContext db = new AppDatabaseContext();

        public async Task<ActionResult> Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Libro).Include(p => p.TipoPrestamo).Include(p => p.Usuario).OrderByDescending(a => a.IDPrestamo);
            return View(await prestamos.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prestamo = await db.Prestamos.Include(p => p.Libro).Include(p => p.TipoPrestamo).Include(p => p.Usuario).FirstAsync(a => a.IDPrestamo == id);

            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        public ActionResult Create()
        {
            Prestamo prestamo = new Prestamo();
            prestamo.FechaPrestamo = DateTime.Now;
            ViewBag.IdLibro = new SelectList(db.Libros, "IDLibro", "Nombre");
            ViewBag.IdTipoPrestamo = new SelectList(db.TiposPrestamo, "IdTipoPrestamo", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombres");
            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPrestamo,IdLibro,IdUsuario,IdTipoPrestamo,FechaPrestamo,FechaDevolucion")] Prestamo prestamo)
        {
            var cantidadPrestamos = await db.Prestamos.CountAsync(a => a.IdUsuario == prestamo.IdUsuario && DbFunctions.TruncateTime(a.FechaPrestamo) == DateTime.Today.Date);

            if (cantidadPrestamos == 3)
            {
                Alerts.Alert(this, "El Usuario ha llegado al limite de sus prestamos diarios.", NotificationType.error);
                return RedirectToAction("Index");
            }

            var usuarioActivo = await db.Usuarios.FindAsync(prestamo.IdUsuario);

            if (usuarioActivo.Estado != true)
            {
                Alerts.Alert(this, "El Usuario se encuentra Sancionado no puede realizar un prestamo.", NotificationType.error);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Prestamos.Add(prestamo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdLibro = new SelectList(db.Libros, "IDLibro", "Nombre", prestamo.IdLibro);
            ViewBag.IdTipoPrestamo = new SelectList(db.TiposPrestamo, "IdTipoPrestamo", "Nombre", prestamo.IdTipoPrestamo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombres", prestamo.IdUsuario);
            return View(prestamo);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = await db.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLibro = new SelectList(db.Libros, "IDLibro", "Nombre", prestamo.IdLibro);
            ViewBag.IdTipoPrestamo = new SelectList(db.TiposPrestamo, "IdTipoPrestamo", "Nombre", prestamo.IdTipoPrestamo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombres", prestamo.IdUsuario);
            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPrestamo,IdLibro,IdUsuario,IdTipoPrestamo,FechaPrestamo,FechaDevolucion")] Prestamo prestamo)
        {


            if (ModelState.IsValid)
            {
                if (prestamo.FechaDevolucion != null)
                {
                    DateTime fechaFin = (DateTime)prestamo.FechaDevolucion?.AddHours(25); 

                    TimeSpan tiempoTranscurrido = fechaFin - prestamo.FechaPrestamo;
                    if (tiempoTranscurrido > TimeSpan.FromHours(24))
                    {
                        var user = await db.Usuarios.FindAsync(prestamo.IdUsuario);

                        user.Estado = false;

                        db.Entry(user).State = EntityState.Modified;
                    }
                }

                db.Entry(prestamo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdLibro = new SelectList(db.Libros, "IDLibro", "Nombre", prestamo.IdLibro);
            ViewBag.IdTipoPrestamo = new SelectList(db.TiposPrestamo, "IdTipoPrestamo", "Nombre", prestamo.IdTipoPrestamo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombres", prestamo.IdUsuario);
            return View(prestamo);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prestamo = await db.Prestamos.Include(p => p.Libro).Include(p => p.TipoPrestamo).Include(p => p.Usuario).FirstAsync(a => a.IDPrestamo == id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prestamo prestamo = await db.Prestamos.FindAsync(id);
            db.Prestamos.Remove(prestamo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DevolverLibro(int id)
        {
            Prestamo prestamo = await db.Prestamos.FindAsync(id);

            DateTime fechaFin = DateTime.Now.AddHours(25); // fechaFin es mayor a fechaInicio por 25 horas



            if (prestamo.FechaDevolucion != null)
            {
                Alerts.Alert(this, "El libro ya tiene una fecha de devolucion asignada.", NotificationType.error);
                return RedirectToAction("Index");
            }

            TimeSpan tiempoTranscurrido = fechaFin - prestamo.FechaPrestamo;
            if (tiempoTranscurrido > TimeSpan.FromHours(24))
            {
                var user = await db.Usuarios.FindAsync(prestamo.IdUsuario);

                user.Estado = false;

                db.Entry(user).State = EntityState.Modified;
            }

            prestamo.FechaDevolucion = DateTime.Now;

            db.Entry(prestamo).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Report()
        {
            var prestamos = await db.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.TipoPrestamo)
                .Include(p => p.Usuario)
                .Select(a => new ReporteViewModel
                {
                    IdPrestamo = a.IDPrestamo,
                    IdLibro = a.IdLibro,
                    NombreLibro = a.Libro.Nombre,
                    FechaPrestamo = a.FechaPrestamo,
                    DniUsuario = a.Usuario.Dni,
                    NombreUsuario = a.Usuario.Nombres,
                    ApellidoUsuario = a.Usuario.Apellidos,
                    TipoUsuario = a.Usuario.TipoUsuario.Nombre,
                    TipoLectura = a.TipoPrestamo.Nombre,
                    FechaDevolucion = a.FechaDevolucion

                }).ToListAsync();

            return View(prestamos);
        }


        public ActionResult Imprimir()
        {
            return new ActionAsPdf("Report")
            { FileName = string.Format("Reporte - {0}.pdf", Guid.NewGuid().ToString()), PageOrientation = Rotativa.Options.Orientation.Landscape };
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
