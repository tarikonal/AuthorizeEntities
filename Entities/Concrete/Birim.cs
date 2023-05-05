using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Birim", Schema = "Yetki")]
    public class Birim : IEntity
    {
        //public Birim()
        //{
        //    BirimAgac = new HashSet<BirimAgac>();
        //    BirimYetkiIslevObje = new HashSet<BirimYetkiIslevObje>();
        //    //Ils_Duyuru_Birim = new HashSet<Ils_Duyuru_Birim>();
        //}
        [Key]
        public long Id { get; set; }
        [MaxLength(50), Column(TypeName = "varchar")] 
        public string KeyValue { get; set; }
        [MaxLength(250), Column(TypeName = "varchar")]
        public string BirimAdi { get; set; }
        public long? ProjeId { get; set; }
        public bool? Durum { get; set; }

        //public virtual Proje Proje { get; set; }
        //public virtual ICollection<BirimAgac> BirimAgac { get; set; }
        //public virtual ICollection<BirimYetkiIslevObje> BirimYetkiIslevObje { get; set; }
       // public virtual ICollection<Ils_Duyuru_Birim> Ils_Duyuru_Birim { get; set; }

    }
}
