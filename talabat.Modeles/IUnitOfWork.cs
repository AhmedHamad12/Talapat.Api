using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;
using talabat.Modeles.Reposituory;

namespace talabat.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IGenericRepo<TEntity> repo<TEntity> () where TEntity : BaseEntity;
        Task<int>CompleteAsync();
    }
}
