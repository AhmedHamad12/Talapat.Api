using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Specificatios
{
    public class productSpecParams
    {
        public string? sort { get; set; }
        public int? productType { get; set; }
        public int? productBrand { get; set; }
        private int pageSize=10;

        public int PageSize 
        {
            get { return pageSize; }
            set { pageSize = value>10?10:value; }
        }
        public int pageIndex { get; set; } = 1;
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }


    }
}
