using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models
{
    [Table("TIPOUSUARIO")]
    public class TipoUsuario
    {
        [Key]
        [Column("ID_TIPOUSUARIO")]
        public int IdTipoUsuario { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

    }
}