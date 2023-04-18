using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models
{
	[Table("LIBROS")]
	public class Libro
	{
		[Key]
		[Column("ID_LIBRO")]
		public int IDLibro { get; set; }

		[Column("NOMBRE")]
		public string Nombre { get; set; }

		[Column("CATEGORIA")]
		public string Categoria { get; set; }

		[Column("AUTOR")]
		public string Autor { get; set; }

		[Column("PAIS")]
		public string Pais { get; set; }

		[Column("FECHA_PUBLICACION")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[Display(Name = "Fecha Publicación")]
		[DataType(DataType.Date)]
		public DateTime FechaPublicacion { get; set; }
		
		[Column("EDITORIAL")]
		public string Editorial { get; set; }
	}
}