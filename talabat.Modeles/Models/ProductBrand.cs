using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Modeles.Models
{
    public class ProductBrand :BaseEntity
    {
        public string name { get; set; }

        //public ICollection<Product> products { get; set; } = new HashSet<Product>;
    }
}
