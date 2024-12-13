using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models;

namespace talabat.Core.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetCustomerAsync(string BasketId);
        Task<CustomerBasket?> UpdateCustomerAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
