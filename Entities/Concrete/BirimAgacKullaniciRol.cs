
using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("BirimAgacKullaniciRol", Schema = "Yetki")]
    public class BirimAgacKullaniciRol :IEntity
    {
        [Key]
        public long Id { get; set; }
        public long? BirimAgacId { get; set; }
        [ForeignKey("KRMKLNTML")]
        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public bool? SureliGorevlendirme { get; set; }
        public DateTime? GorevBaslangicTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public bool? Durum { get; set; }

       // public virtual View_KRMKLNTML KRMKLNTML { get; set; }
        //public virtual BirimAgac BirimAgac { get; set; }
        //public virtual Rol Rol { get; set; }

    }
}
