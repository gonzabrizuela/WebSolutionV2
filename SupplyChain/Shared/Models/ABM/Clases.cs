using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Clases")]
    public class Clase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CG_CLASE { get; set; } = 0;
        public string DES_CLASE { get; set; } = "";
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; } = "";
    }
}
