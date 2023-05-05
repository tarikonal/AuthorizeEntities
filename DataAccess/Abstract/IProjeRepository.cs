
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Dtos;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IProjeRepository : IEntityRepository<Proje>
    {
        Task<List<SelectionItem>> GetProjesLookUp();
        //Task<IEnumerable<SelectionItem>> GetProjesLookUp();
    }
}