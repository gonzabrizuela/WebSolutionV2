using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Lineas")]
    public class Linea
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CG_LINEA { get; set; }
        [Display(Name = "Línea producción")]
        public string DES_LINEA { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
