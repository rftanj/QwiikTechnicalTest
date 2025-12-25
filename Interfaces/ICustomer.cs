using QwiikTechnicalTest.Models.DTO.Customer;

namespace QwiikTechnicalTest.Interfaces
{
    public interface ICustomer
    {
        Task<List<CustomerResponseDTO>?> GetListDataCustomer(ListCustomerRequest request);
        //Task<CustomerResponseDTO> GetCustomerById(int id);
    }
}