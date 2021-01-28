using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Paradas")]
    public class Paradas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CP { get; set; } = 0;
        public string DESCRIP { get; set; } = "";
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; }
    }
}
