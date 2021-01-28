using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("ProTarea")]
    public class Protarea
    {
        [Key]
        public string TAREAPROC { get; set; } = "";
        public string DESCRIP { get; set; } = "";
        public string OBSERVAC { get; set; } = "";
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; } = "";
    }
}
