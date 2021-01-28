using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("TipoArea")]
    public class TipoArea
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CG_TIPOAREA { get; set; }
        [Display(Name = "Tipo de área")]
        public string DES_TIPOAREA { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
