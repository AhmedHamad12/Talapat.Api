using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Specificatios
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        //automatic property
        public Expression<Func<T, bool>> creiteril { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool Ispagination { get; set; }

        public BaseSpecifications(Expression<Func<T,bool>> creiterilExpresion)
        {
           creiteril = creiterilExpresion;
        }
        public BaseSpecifications()
        {
            
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderExbression)
        {
            OrderBy = OrderExbression;
        }
        public void AddOrderByDes(Expression<Func<T, object>> OrderByDesExbression)
        {
            OrderByDescending = OrderByDesExbression;
        }
        public void ApplyPagination(int skip,int take)
        {
            Ispagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
