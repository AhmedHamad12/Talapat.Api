using Talabat.API.DTO;

namespace Talabat.API.Helpers
{
    public class Pagination<T>
    {
      

        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data,int count)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            Data = data;
            Count = count;

        }

        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
