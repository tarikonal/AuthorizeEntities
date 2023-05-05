//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Yetki", Schema = "Yetki")]
    public class Yetki : IEntity
    {
        //public Yetki()
        //{
        //    BirimYetkiIslevObje = new HashSet<BirimYetkiIslevObje>();
        //    RolYetkiIslevObje = new HashSet<RolYetkiIslevObje>();
        //    KullaniciYetkiIslevObje = new HashSet<KullaniciYetkiIslevObje>();
        //    KullaniciYetkiIslevEngel = new HashSet<KullaniciYetkiIslevEngel>();
        //}
        [Key]
        public long Id { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string YetkiAdi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }

        public long? ProjeId { get; set; }
        //public virtual Proje Proje { get; set; }
        //public virtual ICollection<BirimYetkiIslevObje> BirimYetkiIslevObje { get; set; }
        //public virtual ICollection<RolYetkiIslevObje> RolYetkiIslevObje { get; set; }
        //public virtual ICollection<KullaniciYetkiIslevObje> KullaniciYetkiIslevObje { get; set; }
        //public virtual ICollection<KullaniciYetkiIslevEngel> KullaniciYetkiIslevEngel { get; set; }
    }
}
