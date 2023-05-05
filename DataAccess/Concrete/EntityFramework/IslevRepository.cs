
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class IslevRepository : EfEntityRepositoryBase<Islev, ProjectDbContext>, IIslevRepository
    {
        public IslevRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
