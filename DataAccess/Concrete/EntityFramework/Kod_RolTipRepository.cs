
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class Kod_RolTipRepository : EfEntityRepositoryBase<Kod_RolTip, ProjectDbContext>, IKod_RolTipRepository
    {
        public Kod_RolTipRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
