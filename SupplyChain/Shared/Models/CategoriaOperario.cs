using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("CateOperario")]
    public class CategoriaOperario
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CG_CATEOP { get; set; }
        [Display(Name = "Categoria")]
        public string DES_CATEOP { get; set; }
        [Display(Name = "Valor hora")]
        public decimal? VALOR_HORA { get; set; }
        [Display(Name = "Moneda")]
        public string MONEDA { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
