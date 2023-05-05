﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProjeRepository : EfEntityRepositoryBase<Proje, ProjectDbContext>, IProjeRepository
    {
        public ProjeRepository(ProjectDbContext context) : base(context)
        {
        }
        public async Task<List<SelectionItem>> GetProjesLookUp()
        {
            var lookUp = await (from entity in Context.Projes
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Adi
                                }).ToListAsync();
            return lookUp;
        }
    }
}
