
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ProjeRepository : EfEntityRepositoryBase<Proje, ProjectDbContext>, IProjeRepository
    {
        public ProjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
