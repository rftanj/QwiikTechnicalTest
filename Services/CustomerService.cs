using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models.DTO.Customer;
using QwiikTechnicalTest.Repositories;

namespace QwiikTechnicalTest.Services
{
    public class CustomerService : ICustomer
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository repository)
        {
            _customerRepository = repository;
        }

        public async Task<List<CustomerResponseDTO>?> GetListDataCustomer(ListCustomerRequest request)
        {
            try
            {
                var customers = await _customerRepository.GetListDataCustomer(request);
                if (customers.Count == 0)
                {
                    return null;
                }

                var customersDtos = customers.Select(x => new CustomerResponseDTO
                {
                    id = x.Id,
                    name = x.Name,
                    email = x.Email,
                    phone_number = x.PhoneNumber,
                    created_at = x.CreatedAt.ToString("dd MMM yyyy HH:mm"),
                    updated_at = x.UpdatedAt?.ToString("dd MMM yyyy HH:mm") ?? "-"
                }).ToList();

                return customersDtos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        //public async Task<CustomerResponseDTO> GetCustomerById(int id)
        //{
        //    var customer = await _customerRepository.GetCustomerByPhoneNumber(id);
        //    if (customer == null)
        //    {
        //        return null;
        //    }
        //    var customerDto = new CustomerResponseDTO
        //    {
        //        Id = customer.Id,
        //        Name = customer.Name,
        //        Email = customer.Email,
        //        PhoneNumber = customer.PhoneNumber,
        //        CreatedAt = customer.CreatedAt.ToString("ddMMyyyy"),
        //        UpdatedAt = customer.UpdatedAt.ToString("ddMMyyyy")
        //    };
        //    return customerDto;
        //}
    }
}
