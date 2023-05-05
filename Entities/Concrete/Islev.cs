using Core.Entities;
//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Islev", Schema = "Yetki")]
    public class Islev : IEntity
    { 
        //public Islev()
        //{
        //    BirimYetkiIslevObje = new HashSet<BirimYetkiIslevObje>();
        //    RolYetkiIslevObje = new HashSet<RolYetkiIslevObje>();
        //    KullaniciYetkiIslevObje = new HashSet<KullaniciYetkiIslevObje>();
        //}
        [Key]
        public long Id { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string IslevAdi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }
        //public virtual Proje Proje { get; set; }
        //public virtual ICollection<BirimYetkiIslevObje> BirimYetkiIslevObje { get; set; }
        //public virtual ICollection<RolYetkiIslevObje> RolYetkiIslevObje { get; set; }
        //public virtual ICollection<KullaniciYetkiIslevObje> KullaniciYetkiIslevObje { get; set; }
    }
}
