using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Services
{
    public interface IResponseCacheService
    {
        //Cash Data
        Task CashResponse(string CashKey,object Response, TimeSpan Expiretime);



        //Get Cash Data
        Task<string> GetCashResponse(string? CashKey);
    }
}
