﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class BirimAgacRepository : EfEntityRepositoryBase<BirimAgac, ProjectDbContext>, IBirimAgacRepository
    {
        public BirimAgacRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
