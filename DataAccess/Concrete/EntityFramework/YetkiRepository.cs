
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class YetkiRepository : EfEntityRepositoryBase<Yetki, ProjectDbContext>, IYetkiRepository
    {
        public YetkiRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
