
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class KullaniciYetkiIslevObjeRepository : EfEntityRepositoryBase<KullaniciYetkiIslevObje, ProjectDbContext>, IKullaniciYetkiIslevObjeRepository
    {
        public KullaniciYetkiIslevObjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
