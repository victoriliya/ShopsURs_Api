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
    public class CostumerRepository : ICostumerRepository
    {

        private readonly ApplicationDbContext _context;

        public int TotalCount { get; set; }


        public CostumerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<User> GetCustomersAsyn()
        {
            var category = _context.Users.Include(user => user.UserType);

            TotalCount = category.Count();
            return category;
        }

        public async Task<bool> CreateCustomerAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await SavedAsync();
        }

        public async Task<IEnumerable<User>> GetAllCustomerAsync(int page, int perPage)
        {
            var query = await GetCustomersAsyn().Skip((page - 1) * perPage).Take(perPage).ToListAsync();
            return query;
        }

        public async Task<User> GetCustomerByIdAsync(string customerId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerId);
            return  user;
        }

        public async Task<IEnumerable<User>> GetCustomerByNameAsync(string name)
        {
            var user = await _context.Users.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name)).ToListAsync();

            return user;

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


    }
}
