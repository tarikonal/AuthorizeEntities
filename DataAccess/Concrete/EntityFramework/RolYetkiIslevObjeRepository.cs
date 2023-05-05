
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RolYetkiIslevObjeRepository : EfEntityRepositoryBase<RolYetkiIslevObje, ProjectDbContext>, IRolYetkiIslevObjeRepository
    {
        public RolYetkiIslevObjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
