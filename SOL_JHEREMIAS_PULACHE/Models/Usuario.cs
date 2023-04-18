using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models
{
	[Table("USUARIOS")]
	public class Usuario
	{
		[Key]
		[Column("ID_USUARIO")]
		public int IdUsuario { get; set; }

		[Column("DNI")]
		public string Dni { get; set; }

		[Column("NOMBRES")]
		public string Nombres { get; set; }

		[Column("APELLIDOS")]
		public string Apellidos { get; set; }

		[Column("EMAIL")]
		public string Email { get; set; }

		[Column("TELEFONO")]
		public string Telefono { get; set; }

		
		/*[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		[Display(Name = "Fecha Publicación")]
		[DataType(DataType.Date)]*/

		[Column("ESTADO")]
		public bool Estado { get; set; }

		[Column("ID_TIPOUSUARIO")]
        public int IdTipoUsuario { get; set; }

		[ForeignKey(nameof(IdTipoUsuario))]
        public TipoUsuario TipoUsuario { get; set; }


    }
}