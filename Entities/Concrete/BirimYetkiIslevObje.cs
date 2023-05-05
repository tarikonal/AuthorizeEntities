using Core.Entities;
//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{

    [Table("BirimYetkiIslevObje", Schema = "Yetki")]
    public class BirimYetkiIslevObje :  IEntity
    {
        [Key]
        public long Id { get; set; }
        public long? BirimId { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        //public virtual Birim Birim { get; set; }
        //public virtual Yetki Yetki { get; set; }
        //public virtual Islev Islev { get; set; }
        //public virtual Obje Obje { get; set; }
    }
}
