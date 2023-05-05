using Core.Entities;
using System.ComponentModel.DataAnnotations;
//using JugaYetkilendirmeIslemleri.Domain.Entities.BaseClass;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete

{
    [Table("Kod_BirlikTip", Schema = "Yetki")]
    public partial class Kod_BirlikTip : IEntity
    {
        [Key]
        public long Id { get; set; }

    }
}
