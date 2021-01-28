using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Celdas")]
    public class Celda
    {
        [Key]
        [Display(Name = "Codigo")]
        public string CG_CELDA { get; set; }
        [Display(Name = "Celda")]
        public string DES_CELDA { get; set; }
        [Display(Name = "Ärea")]
        public int? CG_AREA { get; set; }
        [Display(Name = "Cat.Operario")]
        public int? CG_CATEOP { get; set; }
        [Display(Name = "Cap.Ilimitada")]
        public int? ILIMITADA { get; set; }
        [Display(Name = "Coef.Uso")]
        public decimal? COEFI { get; set; }
        [Display(Name = "Celda del proveedor")]
        public int CG_PROVE { get; set; }
        [Display(Name = "Valor de amortización")]
        public decimal? VALOR_AMOR { get; set; }
        [Display(Name = "Valor de mercado")]
        public decimal? VALOR_MERC { get; set; }
        [Display(Name = "Moneda")]
        public string MONEDA { get; set; }
        [Display(Name = "Años amortización")]
        public decimal? CANT_ANOS { get; set; }
        [Display(Name = "Cantidad unidad")]
        public decimal? CANT_UNID { get; set; }
        [Display(Name = "Años de reposición")]
        public decimal? REP_ANOS { get; set; }
        [Display(Name = "M2 que ocupa")]
        public decimal? M2 { get; set; }
        [Display(Name = "Energía")]
        public decimal? ENERGIA { get; set; }
        [Display(Name = "Combustible")]
        public decimal? COMBUST { get; set; }
        [Display(Name = "Aire comprimido")]
        public decimal? AIRE_COMP { get; set; }
        [Display(Name = "Tipo celda")]
        public int? CG_TIPOCELDA { get; set; }
        [Display(Name = "Depósito celda")]
        public int? CG_DEPOSM{ get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
