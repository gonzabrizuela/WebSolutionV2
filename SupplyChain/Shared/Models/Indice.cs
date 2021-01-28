using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Indic")]
    public class Indice
    {
        [Display(Name = "Indice")]
        public string DES_IND { get; set; }
        [Display(Name = "Valor venta")]
        public int? VA_INDIC { get; set; }
        [Display(Name = "Valor compra")]
        public int? VA_COMPRA { get; set; }
        [Display(Name = "Fecha valor")]
        public DateTime FE_INDIC { get; set; }
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? REGISTRO { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
