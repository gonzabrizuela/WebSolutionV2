using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("MONEDAS")]
    public class Monedas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MONEDA { get; set; }
        public string SIMBOLO { get; set; }
    }
}
