using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("Cia")]
    public class Compañia
    {
        [Key]
        [Display(Name = "Codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CG_CIA { get; set; }
        [Display(Name = "Compañía")]
        public string DES_CIA { get; set; }
        [Display(Name = "Domicilio fiscal")]
        public string DOM_FISC { get; set; }
        [Display(Name = "Localidad")]
        public string LOC_FISC { get; set; }
        [Display(Name = "Provincia")]
        public string DES_PROV { get; set; }
        [Display(Name = "Codigo zip")]
        public string COD_POST { get; set; }
        [Display(Name = "Pais")]
        public string DES_PAIS { get; set; }
        [Display(Name = "Domicilio comercial")]
        public string DOM_COM { get; set; }
        [Display(Name = "Localidad")]
        public string LOC_COM { get; set; }
        [Display(Name = "Prefijo TE pais")]
        public int? PREFIJO { get; set; }
        [Display(Name = "Teléfono")]
        public string TELEFONO { get; set; }
        [Display(Name = "CUIT")]
        public string CUIT { get; set; }
        [Display(Name = "IIBB")]
        public string IIBB { get; set; }
        [Display(Name = "Actividad")]
        public string ACTIVIDAD { get; set; }
        [Display(Name = "Fecha contratación")]
        public DateTime FE_CONTRAT { get; set; }
        [Display(Name = "Fecha desvinculación")]
        public string FE_CIERRE { get; set; }
        [Display(Name = "Aviso")]
        public string AVISO { get; set; }




    }
}
