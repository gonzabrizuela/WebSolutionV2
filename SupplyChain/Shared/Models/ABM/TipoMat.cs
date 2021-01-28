using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("TipoMat")]
	public class TipoMat
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string TIPO { get; set; } = "";
		public int CG_CIA { get; set; } = 0;
		public string USUARIO { get; set; } = "";

	}
}
