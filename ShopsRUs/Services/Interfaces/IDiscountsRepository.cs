using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Services.Interfaces
{
    public interface IDiscountsRepository
    {

        public int TotalCount { get; set; }
        public Task<IEnumerable<UserType>> GetAllDiscountAsync(int page, int perPage);
        public Task<UserType> GetDiscountPercentageByTypeAsync(string discountType);
        public Task<bool> AddDiscount(Discount discount, string discountTypeName);
        public Task<UserType> GetDiscountUserType(string discountId);
        public Task<bool> SavedAsync();

    }
}
