using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SupplyChain.Shared.Models
{
    public class StockG
    {
        [Display(Name = "Vale")]
        public decimal? VALE { get; set; }
        [Display(Name = "Fecha vale")]
        public DateTime FE_MOV { get; set; }
        [Display(Name = "Asiento contable")]
        public decimal? VOUCHER { get; set; }
        [Display(Name = "Comprobante")]
        [DefaultValue("")]
        public string COMPROB { get; set; }
        [Display(Name = "Factura")]
        [DefaultValue("")]
        public string FACTURA { get; set; }
        [Display(Name = "Letra de la factura")]
        [DefaultValue("")]
        public string LEYENDA { get; set; }
        [Display(Name = "Remito")]
        [DefaultValue("")]  
        public string REMITO { get; set; }
        [Display(Name = "Tipo de operación")]
        public int? TIPOO { get; set; }
        [Display(Name = "Pedido")]
        public decimal? PEDIDO { get; set; }
        [Display(Name = "Orden de compra cliente")]
        public decimal? NUMOCI { get; set; }
        [Display(Name = "Orden de compra proveedor")]
        public decimal? OCOMPRA { get; set; }
        [Display(Name = "Orden de fabricación")]
        public decimal? CG_ORDF { get; set; }
        [Display(Name = "Observaciones")]
        [DefaultValue("")]
        public string OBSERVACIONES { get; set; }
        [DefaultValue("")]
        [Display(Name = "Especificaciones")]
        public string OBSERITEM { get; set; }
        [DefaultValue("")]
        [Display(Name = "Observaciones")]
        public string OBS1 { get; set; } 
        [DefaultValue("")]
        [Display(Name = "Observaciones")]
        public string OBS2 { get; set; } 
        [DefaultValue("")]
        [Display(Name = "Observaciones")]
        public string OBS3 { get; set; }
        [Display(Name = "Observaciones")]
        [DefaultValue("")]
        public string OBS4 { get; set; }
        [Display(Name = "Aviso")] 
        [DefaultValue("")]
        public string AVISO { get; set; }
        [Display(Name = "Dirección entrega")] 
        [DefaultValue("")]
        public string DIRENT { get; set; }
        [Display(Name = "Tipo insumo")]
        public int? CG_ORDEN { get; set; }
        [Display(Name = "Código artículo")] 
        [DefaultValue("")]
        public string CG_ART { get; set; }
        [Display(Name = "Nombre artículo")]
        public string DES_ART { get; set; }
        [Display(Name = "Tipo artículo")]
        public string TIPO { get; set; }
        [Display(Name = "Lote")]
        public string LOTE { get; set; }
        [Display(Name = "Serie")]
        public string SERIE { get; set; }
        [Display(Name = "Despacho")]
        public string DESPACHO { get; set; }
        [Display(Name = "Ubicación")]
        public string UBICACION { get; set; }
        [Display(Name = "Depósito")]
        public int? CG_DEP { get; set; }
        [Display(Name = "Cantidad")]
        public decimal? CANTENT { get; set; }
        [Display(Name = "Cantidad operación")]
        public decimal? STOCK { get; set; }
        [Display(Name = "Unidad stock")]
        public string UNID { get; set; }
        [Display(Name = "Factor conversión")]
        public decimal? CG_DEN { get; set; }
        [Display(Name = "Cantidad comercial")]
        public decimal? STOCKA { get; set; }
        [Display(Name = "Unidad comercial")]
        [DefaultValue("")]
        public string UNIDA { get; set; }
        [Display(Name = "Cantidad comercial")]
        public decimal? CANTENTA { get; set; }
        [Display(Name = "Fecha entrega")]
        public DateTime ENTRREAL { get; set; }
        [Display(Name = "Moneda")]
        [DefaultValue("PESOS")]
        public string MONEDA { get; set; }
        [Display(Name = "Precio unitario")]
        public decimal? IMPORTE1 { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Precio total")]
        public decimal? IMPORTE2 { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Importe del descuento")]
        public decimal? IMPORTE3 { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Precio total con descuento")]
        public decimal? IMPORTE4 { get; set; }
        [Display(Name = "Precio")]
        public decimal? IMPORTE6 { get; set; }
        [Display(Name = "Porciento descuento")]
        public decimal? DESCUENTO { get; set; }
        [Display(Name = "Porciento bonificación")]
        public decimal? BONIFIC { get; set; }
        [Display(Name = "Porciento IVA")]
        public decimal? IVA { get; set; }
        [Display(Name = "Indice conversión moneda")]
        public decimal? VA_INDIC { get; set; }
        [Display(Name = "Cuenta contable")]
        public decimal? CG_CUENT { get; set; }
        [Display(Name = "CUIT")]
        [DefaultValue("")]
        public string CUIT { get; set; }
        [Display(Name = "Código proveedor")]
        public int? CG_PROVE { get; set; }
        [Display(Name = "Nombre proveedor")]
        [DefaultValue("")]
        public string DES_PROVE { get; set; }
        [Display(Name = "Código cliente")]
        public int? CG_CLI { get; set; }
        [Display(Name = "Nombre cliente")]
        [DefaultValue("")]
        public string DES_CLI { get; set; }
        [Display(Name = "Dirección")]
        [DefaultValue("")]
        public string DIRECC { get; set; }
        [Display(Name = "Localidad")]
        [DefaultValue("")]
        public string LOCALIDAD { get; set; }
        [Display(Name = "Código postal")]
        [DefaultValue("")]
        public string CG_POSTA { get; set; }
        [Display(Name = "Orden compra cliente")]
        [DefaultValue("")]
        public string ORCO { get; set; }
        [Display(Name = "Cantidad pedida")]
        public decimal? CANTPED { get; set; }
        [Display(Name = "Flag")]
        public int? FLAG { get; set; }
        [Display(Name = "Usuario")]
        [DefaultValue("")]
        public string USUARIO { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }
    }
}
