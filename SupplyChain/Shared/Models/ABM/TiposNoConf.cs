using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("TiposNoConf")]
    public class TiposNoConf
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cg_TipoNc { get; set; } = 0;//
        public string Des_TipoNc { get; set; } = "";//
        public int CG_CIA { get; set; } = 0;//
        public string USUARIO { get; set; } = "";
    }
}
