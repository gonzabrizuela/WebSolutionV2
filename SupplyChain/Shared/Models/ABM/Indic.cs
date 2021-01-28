using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Indic")]
    public class Indic
    {
        public string DES_IND { get; set; } = "";
        public decimal VA_INDIC { get; set; } = 0;
        public decimal VA_COMPRA { get; set; } = 0;
        public DateTime FE_INDIC { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int REGISTRO { get; set; } = 0;
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; } = "";
    }
}




      