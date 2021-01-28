using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("CateOperarios")]
    public class CatOpe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CG_CATEOP { get; set; } = "";
        public string DES_CATEOP { get; set; } = "";
        public decimal VALOR_HORA { get; set; } = 0;
        public string MONEDA { get; set; } = "";
        public int CG_CIA { get; set; } = 0;//
        public string USUARIO { get; set; } = "";
    }
}
