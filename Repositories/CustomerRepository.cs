using QwiikTechnicalTest.Models.DB;
using QwiikTechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using QwiikTechnicalTest.Models.DTO.Customer;

namespace QwiikTechnicalTest.Repositories
{
    public class CustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetListDataCustomer(ListCustomerRequest request)
        {
            return await _context.Customers.Where(x => request.customer_id == null || x.Id == request.customer_id).ToListAsync();
        }

        public async Task<Customer?> GetCustomerByPhoneNumber(string phone_number)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.PhoneNumber == phone_number);
        }

        public async Task<int> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }


    }
}
