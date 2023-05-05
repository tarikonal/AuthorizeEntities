
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class Kod_BirlikTipRepository : EfEntityRepositoryBase<Kod_BirlikTip, ProjectDbContext>, IKod_BirlikTipRepository
    {
        public Kod_BirlikTipRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
