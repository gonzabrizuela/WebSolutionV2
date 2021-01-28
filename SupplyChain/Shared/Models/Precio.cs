using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Precios")]
    public class Precio
    {
        [Key]
        [Display(Name = "Código artículo")]
        public string CG_ART { get; set; }
        [Display(Name = "Nombre artículo")]
        public string DES_ART { get; set; }
        [Display(Name = "Tipo insumo")]
        public int? CG_ORDEN { get; set; }
        [Display(Name = "Unidad stock")]
        public string UNID { get; set; }
        [Display(Name = "Lista A pesos")]
        public decimal? IMPORTE1 { get; set; }
        [Display(Name = "Lista A dólares")]
        public decimal? IMPORTE2 { get; set; }
        [Display(Name = "Lista B pesos")]
        public decimal? IMPB1 { get; set; }
        [Display(Name = "Lista B dólares ")]
        public decimal? IMPB2 { get; set; }
        [Display(Name = "Lista C pesos")]
        public decimal? IMPC1 { get; set; }
        [Display(Name = "Lista C dólares")]
        public decimal? IMPC2 { get; set; }
        [Display(Name = "Lista X pesos")]
        public decimal? IMPORTE61 { get; set; }
        [Display(Name = "Lista X dólares")]
        public decimal? IMPORTE62 { get; set; }
        [Display(Name = "Descuento")]
        public decimal? DESCUENTO { get; set; }
        [Display(Name = "Exento de IVA")]
        public string EXEN { get; set; }
        [Display(Name = "Marca")]
        public string MARCA { get; set; }
        [Display(Name = "Punto de reposición")]
        public decimal? REPOS { get; set; }
        [Display(Name = "Costo en pesos")]
        public decimal? COSTO1 { get; set; }
        [Display(Name = "Costo en dólares")]
        public decimal? COSTO2 { get; set; }
        [Display(Name = "Cuenta contable")]
        public decimal? CG_CUENT { get; set; }
        [Display(Name = "Porciento de IVA")]
        public decimal? IVA { get; set; }
        [Display(Name = "Activo")]
        public string ACTIVO { get; set; }
        [Display(Name = "Especificaciones")]
        public string OBSERITEM { get; set; }
        [Display(Name = "Fecha alta")]
        public DateTime FE_ALTA { get; set; }
        [Display(Name = "Usuario")]
        public string USUARIO { get; set; }
        [Display(Name = "Fecha registro")]
        public DateTime FE_REG { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
