
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class BirimAgacKullaniciRolRepository : EfEntityRepositoryBase<BirimAgacKullaniciRol, ProjectDbContext>, IBirimAgacKullaniciRolRepository
    {
        public BirimAgacKullaniciRolRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
