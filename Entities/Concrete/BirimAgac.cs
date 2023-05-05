//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
//using JugaYetkilendirmeIslemleri.Domain.Entities.Views;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("BirimAgac", Schema = "Yetki")]
    public class BirimAgac :  IEntity
    {
        //public BirimAgac()
        //{
        //    BirimAgacKullaniciRol = new HashSet<BirimAgacKullaniciRol>();
        //}
        [Key]
        public long Id { get; set; }
        public long? BirlikId { get; set; }
        public long? BirimId { get; set; }
        public bool? UyaptaGorunmeDurumu { get; set; }
        public bool? Durum { get; set; }
        //public virtual View_BIRLIK Birlik { get; set; }
        //public virtual Birim Birim { get; set; }

        //public virtual ICollection<BirimAgacKullaniciRol> BirimAgacKullaniciRol { get; set; }
    }
}
