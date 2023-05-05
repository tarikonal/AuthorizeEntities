using Core.Entities;
//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
//using JugaYetkilendirmeIslemleri.Domain.Entities.Views;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{

    [Table("KullaniciYetkiEngel", Schema = "Yetki")]
    public class KullaniciYetkiIslevEngel : IEntity
    {
        [Key]
        public long Id { get; set; }

        public long YetkiId { get; set; }
        public long? IslevId { get; set; }
        [ForeignKey("KRMKLNTML")]
        public int KRMKLNKOD { get; set; }
        public bool Durum { get; set; }


        //public virtual Yetki Yetki { get; set; }
        //public virtual Islev Islev { get; set; }
        //public virtual View_KRMKLNTML KRMKLNTML { get; set; }
    }
}
