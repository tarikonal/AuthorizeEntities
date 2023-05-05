
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ObjeRepository : EfEntityRepositoryBase<Obje, ProjectDbContext>, IObjeRepository
    {
        public ObjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
