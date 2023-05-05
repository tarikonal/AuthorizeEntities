
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class KullaniciYetkiIslevEngelRepository : EfEntityRepositoryBase<KullaniciYetkiIslevEngel, ProjectDbContext>, IKullaniciYetkiIslevEngelRepository
    {
        public KullaniciYetkiIslevEngelRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
