
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RolMenuIslevObjeRepository : EfEntityRepositoryBase<RolMenuIslevObje, ProjectDbContext>, IRolMenuIslevObjeRepository
    {
        public RolMenuIslevObjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
