using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("TURNOS")]
    public class Turnos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CG_TURNO { get; set; } = 0;
        public string TURNO { get; set; } = "";
        public DateTime DESDE { get; set; }
        public DateTime HASTA { get; set; }
    }
}
