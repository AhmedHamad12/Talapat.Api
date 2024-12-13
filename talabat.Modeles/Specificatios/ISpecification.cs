using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Specificatios
{
    public interface ISpecification<T>where T : BaseEntity
    {
        //sign for property for where condition [where (p=>p.Id==id)]=.boll =>func delgate =>Expression
        //signeture for property
        public Expression<Func<T,bool>> creiteril  { get; set; }

        //sign of property for list of includes[include(p=>p.productbrand).include(p=>p.producttype]=>Expression=>Func<T,Object>
        public List<Expression<Func<T,object>>> Includes { get; set; }
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool Ispagination { get; set; }
    }
}
