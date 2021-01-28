using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Paises")]
    public class Pais
    {
        public int Codigo { get; set; } = 0;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cg_Pais { get; set; } = 0;
        public string Des_Pais { get; set; } = "";
        public bool Mercosur { get; set; } = false;
        public string Cuit { get; set; } = "";
    }
}




      