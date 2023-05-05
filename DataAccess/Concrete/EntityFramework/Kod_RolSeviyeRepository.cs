
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class Kod_RolSeviyeRepository : EfEntityRepositoryBase<Kod_RolSeviye, ProjectDbContext>, IKod_RolSeviyeRepository
    {
        public Kod_RolSeviyeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
