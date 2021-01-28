using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Pais")]
    public class pais
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Codigo { get; set; }
        [Display(Name = "Pais")]
        public string Des_Pais { get; set; }
        [Display(Name = "Es Mercosur")]
        public int? Mercosur { get; set; }
        [Display(Name = "CUIT")]
        public string Cuit { get; set; }


    }
}
