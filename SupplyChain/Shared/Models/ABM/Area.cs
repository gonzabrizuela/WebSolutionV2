using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Areas")]
    public class Area
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CG_AREA { get; set; } = 0;
        public string DES_AREA { get; set; } = "";
        public int CG_TIPOAREA { get; set; } = 0;
        public int CG_PROVE { get; set; } = 0;
        public int CG_CIA { get; set; } = 0;
        public string USUARIO { get; set; } = "";
    }
}