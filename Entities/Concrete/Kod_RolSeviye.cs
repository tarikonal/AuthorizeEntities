using Core.Entities;
using Microsoft.EntityFrameworkCore;
//using JugaYetkilendirmeIslemleri.Domain.Entities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete

{
    [Table("Kod_RolSeviye", Schema = "Yetki")]
    public partial class Kod_RolSeviye : IEntity
    {
        [Key]
        public long Id { get; set; }
        public int SeviyeKodu { get; set; }

        //public virtual ICollection<Rol> Rol { get; set; }
    }
}
