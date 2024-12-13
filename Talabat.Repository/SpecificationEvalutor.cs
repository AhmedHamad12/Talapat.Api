using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Specificatios;
using talabat.Modeles.Models;

namespace Talabat.Repository
{
   public static class SpecificationEvalutor<T>where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> input,ISpecification<T> spec)
        {
            var Query = input;
            if (spec.creiteril is not null)  
            { 
                Query=Query.Where(spec.creiteril);
            } 
            if(spec.OrderBy is not null)
                Query=Query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending is not null)
                Query=Query.OrderByDescending(spec.OrderByDescending);
            if(spec.Ispagination)
            {
                Query=Query.Skip(spec.Skip).Take(spec.Take);
            }
            Query=spec.Includes.Aggregate(Query,(CurrentQuery,IncludeExpression)=>CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
