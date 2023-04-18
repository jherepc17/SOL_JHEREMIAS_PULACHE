using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models.ViewModel
{
    public class ReporteViewModel
    {
        public int IdPrestamo { get; set; }
        public int IdLibro { get; set; }
        public string NombreLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string DniUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string TipoLectura { get; set; }
        public DateTime? FechaDevolucion { get; set; }

    }
}