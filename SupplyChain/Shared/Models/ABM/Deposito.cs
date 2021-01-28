using SupplyChain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Depos")]
    public class Deposito
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CG_DEP { get; set; } = 0;
        [Required(ErrorMessage = "Ingresar Deposito")]
        public string DES_DEP { get; set; } = "";
        public string TIPO_DEP { get; set; } = "";
        public int CG_CLI { get; set; } = 0;
        public int CG_PROVE { get; set; } = 0;
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; } = "";
    }
}
