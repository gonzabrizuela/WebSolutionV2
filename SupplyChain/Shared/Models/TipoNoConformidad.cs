using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("TiposNoConf")]
    public class TipoNoConformidad
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Cg_TipoNc { get; set; }
        [Display(Name = "No conformidad")]
        public string Des_TipoNc { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
