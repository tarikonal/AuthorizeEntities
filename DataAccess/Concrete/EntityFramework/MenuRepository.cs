
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class MenuRepository : EfEntityRepositoryBase<Menu, ProjectDbContext>, IMenuRepository
    {
        public MenuRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
