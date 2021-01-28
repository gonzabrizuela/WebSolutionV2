using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Shared.Models
{
    [Table("ProTarea")]
    public class TareaProceso
    {
        [Key]
        [Display(Name = "Codigo")]
        public string TAREAPROC { get; set; }
        [Display(Name = "Tarea del proceso")]
        public string DESCRIP { get; set; }
        [Display(Name = "Observaciones")]
        public string OBSERVAC { get; set; }
        [Display(Name = "Compañía")]
        public int? CG_CIA { get; set; }


    }
}
