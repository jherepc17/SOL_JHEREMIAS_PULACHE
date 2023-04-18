using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOL_JHEREMIAS_PULACHE.Models
{
    [Table("TIPOSPRESTAMO")]
    public class TipoPrestamo
    {
        [Key]
        [Column("ID_TIPOPRESTAMO")]
        public int IdTipoPrestamo { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}