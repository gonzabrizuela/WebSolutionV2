using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Solution")]
    public class Solution
    {
        [Key]
        [Display(Name = "Codigo")]
        public string CAMPO { get; set; }
        [Display(Name = "Tipo")]
        public string TIPO { get; set; }
        [Display(Name = "Valor numérico")]
        public decimal? VALORN { get; set; }
        [Display(Name = "Valor alfanumérico")]
        public string VALORC { get; set; }
        [Display(Name = "Nombre del campo")]
        public string DESCRIP { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
