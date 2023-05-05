//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete

{
    [Table("Menu", Schema = "Yetki")]
    public class Menu : IEntity
    {
        //public Menu()
        //{
        //    RolMenuIslevObje = new HashSet<RolMenuIslevObje>();
        //    KullaniciMenuIslevEngel = new HashSet<KullaniciMenuIslevEngel>();
        //}
        [Key]
        public long Id { get; set; }
        public long? UstMenuId { get; set; }
        public long? ProjeId { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string Adi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Aciklama { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Url { get; set; }
        public int Sira { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string Icon { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText1 { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText2 { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText3 { get; set; }
        public bool? Durum { get; set; }

        //public virtual Proje Proje { get; set; }
        //public virtual Menu UstMenu { get; set; }

        //public virtual ICollection<RolMenuIslevObje> RolMenuIslevObje { get; set; }
        //public virtual ICollection<KullaniciMenuIslevEngel> KullaniciMenuIslevEngel { get; set; }

    }
}
