using Microsoft.EntityFrameworkCore;
using ShopsRUs.Data;
using ShopsRUs.Models;
using ShopsRUs.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Services.Implementations
{
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly ApplicationDbContext _context;
        public int TotalCount { get; set; }


        public DiscountsRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        private IQueryable<UserType> GetDiscountsAsync()
        {
            var category = _context.UserTypes.Include(u => u.Discount);

            TotalCount = category.Count();
            return category;
        }

        public async Task<bool> AddDiscount(Discount discount, string discountName)
        {
            try
            {
                var userType = new UserType
                {
                    DiscountId = discount.Id,
                    UserTypeName = discountName
                };

                await _context.UserTypes.AddAsync(userType);
                await _context.Discounts.AddAsync(discount);

                return await SavedAsync();


            }
            catch (Exception e)
            {

                return false;
            }

       

        }

        public async Task<IEnumerable<UserType>> GetAllDiscountAsync(int page, int perPage)
        {
            var query = await GetDiscountsAsync().Skip((page - 1) * perPage).Take(perPage).ToListAsync();
            return query;
        }

        public async Task<UserType> GetDiscountPercentageByTypeAsync(string discountType)
        {
            var userType = await _context.UserTypes.Include(u => u.Discount).FirstOrDefaultAsync(u => u.UserTypeName.Contains(discountType));
            return userType;
        }

        public async Task<bool> SavedAsync()
        {
            var valueToReturned = false;
            if (await _context.SaveChangesAsync() > 0)
                valueToReturned = true;
            else
                valueToReturned = false;

            return valueToReturned;
        }

        public async Task<UserType> GetDiscountUserType(string discountId)
        {
            return await _context.UserTypes.FirstOrDefaultAsync(u => u.DiscountId == discountId);
        }
    }
}
