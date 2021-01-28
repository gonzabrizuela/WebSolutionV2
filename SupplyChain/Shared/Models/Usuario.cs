using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Contras { get; set; }
    }
}
