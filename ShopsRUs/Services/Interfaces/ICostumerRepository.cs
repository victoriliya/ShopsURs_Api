using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Services.Interfaces
{
    public interface ICostumerRepository
    {

        public int TotalCount { get; set; }
        public Task<IEnumerable<User>> GetAllCustomerAsync(int page, int perPage);
        public Task<bool> CreateCustomerAsync(User user);
        public Task<User> GetCustomerByIdAsync(string customerId);
        public Task<IEnumerable<User>> GetCustomerByNameAsync(string name);
        public Task<bool> SavedAsync();


    }
}


