
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class KullaniciRolRepository : EfEntityRepositoryBase<KullaniciRol, ProjectDbContext>, IKullaniciRolRepository
    {
        public KullaniciRolRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
