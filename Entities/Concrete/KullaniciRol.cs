//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
//using JugaYetkilendirmeIslemleri.Domain.Entities.Views;
using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("KullaniciRol", Schema = "Yetki")]
    public class KullaniciRol : IEntity
    { 
        [Key]
        public long Id { get; set; }
        [ForeignKey("KRMKLNTML")]
        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public long? BirlikId { get; set; }

        [ForeignKey("IdariBirim")]
        public int? IDRBRMKOD { get; set; }

        public bool? Durum { get; set; }

        //public virtual View_KRMKLNTML KRMKLNTML { get; set; }
        //public virtual View_BIRLIK Birlik { get; set; }
        //public virtual View_IDRBRMTML IdariBirim { get; set; }
        //public virtual Rol Rol { get; set; }

    }
}
