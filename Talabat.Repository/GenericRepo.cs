using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Specificatios;
using talabat.Modeles.Models;
using talabat.Modeles.Reposituory;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity //should but constrains and public 
    {
        private readonly StoreContext _dbcontext;

        public GenericRepo(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
          {
            
           return  await _dbcontext.Set<T>().ToListAsync(); 
          }
        public async Task<T> GetByIdAsync(int id)
        
           => await _dbcontext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplaySpecification(specification).ToListAsync();
        }


        public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification)
        {
         return  await ApplaySpecification(specification).FirstOrDefaultAsync();
        }

        //علشان مككرش فى الكود

        public async Task<int> GetCountBySpecAsync(ISpecification<T> specification)
        {
           return await ApplaySpecification(specification).CountAsync();
        }
        private IQueryable<T>ApplaySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }

        public async Task AddAsync(T Item)
        
           => await _dbcontext.AddAsync(Item);

        public void Delete(T item)
        =>_dbcontext.Set<T>().Remove(item);

        public void Update(T item)
        =>_dbcontext.Set<T>().Update(item);
    }
}
