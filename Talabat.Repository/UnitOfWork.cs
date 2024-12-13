using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Modeles.Models;
using talabat.Modeles.Reposituory;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repository;

        public UnitOfWork(StoreContext dbContext )
        {
            _dbContext = dbContext;
            _repository = new Hashtable();
        }
        public async Task<int> CompleteAsync()
            =>  await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
        

        public IGenericRepo<TEntity> repo<TEntity>() where TEntity : BaseEntity
        {
            var type= typeof(TEntity).Name;
            if(!_repository.ContainsKey(type))
            {
            var Repository=new GenericRepo<TEntity>(_dbContext);
                _repository.Add(type, Repository);
            }
            return (IGenericRepo<TEntity>)_repository[type];
        }
    }
}
