using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Specificatios;
using talabat.Modeles.Models;

namespace talabat.Modeles.Reposituory
{
    public interface IGenericRepo<T> where T : BaseEntity //to sure is a table in database
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T>GetByIdAsync(int id);
       
        
        
        
        Task<IReadOnlyList<T>>GetAllWithSpecAsync(ISpecification<T> specification);
        Task<T>GetByIdWithSpecificationAsync(ISpecification<T> specification);
        Task<int>GetCountBySpecAsync(ISpecification<T> specification);
        Task AddAsync(T Item);
        void Delete(T item);
        void Update (T item);

    }
}
