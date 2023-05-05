//using Juga.Abstractions.Data.AuditProperties;
//using Juga.Abstractions.Data.Entities;
//using JugaYetkilendirmeIslemleri.Domain.Entities.DuyuruEntities;
using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Proje", Schema = "Yetki")]
    public class Proje : IEntity
    {
        //public Proje()
        //{
        //    BirimAgac = new HashSet<BirimAgac>();
        //    Rol = new HashSet<Rol>();
        //    Menu = new HashSet<Menu>();
        //    //Ils_Duyuru_Proje = new HashSet<Ils_Duyuru_Proje>();

        //}
        [Key]
        public long Id { get; set; }
        public long? UstProjeId { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string Adi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string Aciklama { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string UrlAdresi { get; set; }
        [MaxLength(500), Column(TypeName = "varchar")]
        public string BakimUrlAdresi { get; set; }
        //public long? AgId { get; set; }
        //public long? TipId { get; set; }
        [MaxLength(50),Column(TypeName ="varchar")]
        public string Logo { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string Icon { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText1 { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText2 { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string IconText3 { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string Ico { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")]
        public string KullanimKlavuzu { get; set; }
        public bool? Durum { get; set; }

        //public virtual Proje UstProje { get; set; }
        //public virtual ICollection<Yetki> Yetki { get; set; }
        //public virtual ICollection<Islev> Islev { get; set; }
        //public virtual ICollection<Obje> Obje { get; set; }
        //public virtual ICollection<Menu> Menu { get; set; }
        //public virtual ICollection<BirimAgac> BirimAgac { get; set; }
        //public virtual ICollection<Rol> Rol { get; set; }


        //public virtual ICollection<Ils_Duyuru_Proje> Ils_Duyuru_Proje { get; set; }

    }
}
