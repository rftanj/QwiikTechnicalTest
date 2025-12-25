using Microsoft.AspNetCore.Mvc;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DTO.Customer;
using QwiikTechnicalTest.Utilities;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QwiikTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerService;
        public CustomerController(ICustomer customer) 
        {
            _customerService = customer;
        }


        [HttpPost("list-customer")]
        public async Task<IActionResult> GetListDataCustomer([FromBody] ListCustomerRequest request)
        {
            try
            {
                var dataCustomers = await _customerService.GetListDataCustomer(request);
                if (dataCustomers is null)
                {
                    return Ok(GeneralResponse<string>.Fail(Constants.NotFoundMessage));
                }

                return Ok(GeneralResponse<List<CustomerResponseDTO>>.Success(dataCustomers));

            }
            catch (Exception ex)
            {
                return BadRequest(GeneralResponse<string>.Fail(Constants.GeneralErrorMessage));
            }
        }
    }
}
