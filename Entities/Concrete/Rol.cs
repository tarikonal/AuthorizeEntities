//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Rol", Schema = "Yetki")]
    public class Rol : IEntity
    {
        //public Rol()
        //{
        //    BirimAgacKullaniciRol = new HashSet<BirimAgacKullaniciRol>();
        //    RolYetkiIslevObje = new HashSet<RolYetkiIslevObje>();
        //    RolMenuIslevObje = new HashSet<RolMenuIslevObje>();
        //    KullaniciRol = new HashSet<KullaniciRol>();
        //}
        [Key]
        public long Id { get; set; }
        public long? ProjeId { get; set; }
        public long? RolTipiId { get; set; }
        public long? RolSeviyeId { get; set; }

        [MaxLength(50), Column(TypeName = "varchar")]
        public string KeyValue { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string RolAdi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Aciklama { get; set; }
        public bool? VarsayilanMi { get; set; }
        public bool? Durum { get; set; }
        //public virtual Kod_RolTip RolTipi { get; set; }
        //public virtual Kod_RolSeviye RolSeviye { get; set; }
        //public virtual Proje Proje { get; set; }
        //public virtual ICollection<BirimAgacKullaniciRol> BirimAgacKullaniciRol { get; set; }
        //public virtual ICollection<RolYetkiIslevObje> RolYetkiIslevObje { get; set; }
        //public virtual ICollection<RolMenuIslevObje> RolMenuIslevObje { get; set; }

        //public virtual ICollection<KullaniciRol> KullaniciRol { get; set; }

    }
}
