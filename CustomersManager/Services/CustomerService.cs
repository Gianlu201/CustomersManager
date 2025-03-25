using CustomersManager.Data;
using CustomersManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomersManager.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Customer>?> GetCustomersAsync()
        {
            try
            {
                return await _context.Customers.AsNoTracking().ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var existingCustomer = await GetCustomerByIdAsync(id);

                if (existingCustomer == null)
                {
                    return false;
                }

                _context.Customers.Remove(existingCustomer);

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
        {
            try
            {
                var existingCustomer = await GetCustomerByIdAsync(id);

                if (existingCustomer == null)
                {
                    return false;
                }

                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.EmailAddress = customer.EmailAddress;

                return await TrySaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
