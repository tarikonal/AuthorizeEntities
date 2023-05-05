
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RolRepository : EfEntityRepositoryBase<Rol, ProjectDbContext>, IRolRepository
    {
        public RolRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
