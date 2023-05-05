using Core.Entities;
//using JugaYetkilendirmeIslemleri.Domain.Entities.BaseClass;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Kod_RolTip", Schema = "Yetki")]
    public partial class Kod_RolTip : IEntity
    {

        [Key]
        public long Id { get; set; }
        //public virtual ICollection<Rol> Rol { get; set; }
    }
}
