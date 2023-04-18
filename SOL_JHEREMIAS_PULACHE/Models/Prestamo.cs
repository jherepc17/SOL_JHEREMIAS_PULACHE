using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models
{
	[Table("PRESTAMOS")]
	public class Prestamo
	{
		[Key]
		[Column("ID_PRESTAMO")]
		public int IDPrestamo { get; set; }

		[Column("ID_LIBRO")]
		public int IdLibro { get; set; }

		[Column("ID_USUARIO")]
		public int IdUsuario { get; set; }

		[Column("ID_TIPO_PRESTAMO")]
		public int IdTipoPrestamo { get; set; }

		[Column("FECHA_PRESTAMO")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
		[Display(Name = "Fecha Prestamo")]
		public DateTime FechaPrestamo { get; set; }

		[Column("FECHA_DEVOLUCION")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
		[Display(Name = "Fecha Devolución")]
		public DateTime? FechaDevolucion { get; set; }

		[ForeignKey(nameof(IdLibro))]
        public Libro Libro { get; set; }

		[ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }

		[ForeignKey(nameof(IdTipoPrestamo))]
        public TipoPrestamo TipoPrestamo { get; set; }
    }
}