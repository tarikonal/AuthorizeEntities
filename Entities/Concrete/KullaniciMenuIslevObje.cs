using Core.Entities;
//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{

    [Table("KullaniciMenuIslevObje", Schema = "Yetki")]
    public class KullaniciMenuIslevObje : IEntity
    {
        [Key]
        public long Id { get; set; }
        public long? KRMKLNKOD { get; set; }
        public long? MenuId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        //public virtual Menu Menu { get; set; }
        //public virtual Islev Islev { get; set; }
        //public virtual Obje Obje { get; set; }
    }
}
