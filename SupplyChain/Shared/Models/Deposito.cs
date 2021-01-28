using SupplyChain.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Depos")]
    public class Deposito
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CG_DEP { get; set; }
        [Display(Name = "Depósito")]
        [Required(ErrorMessage ="Ingresar Deposito")]
        public string DES_DEP { get; set; }
        [Display(Name = "Tipo")]
        public string TIPO_DEP { get; set; }
        [Display(Name = "de Cliente")]
        public int? CG_CLI { get; set; }
        [Display(Name = "de Proveedor")]
        public int? CG_PROVE { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
