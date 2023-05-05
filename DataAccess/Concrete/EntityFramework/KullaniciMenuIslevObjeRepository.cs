﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class KullaniciMenuIslevObjeRepository : EfEntityRepositoryBase<KullaniciMenuIslevObje, ProjectDbContext>, IKullaniciMenuIslevObjeRepository
    {
        public KullaniciMenuIslevObjeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
