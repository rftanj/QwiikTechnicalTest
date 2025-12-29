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


        /// <summary>
        /// Get list of customers.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves customer data based on optional filter:
        /// - customer_id
        ///
        /// This endpoint always returns HTTP 200.
        /// Business outcome is indicated by the 'status' field in the response body.
        /// </remarks>
        /// <param name="request">Filter parameters for listing customers</param>
        /// <response code="200">
        /// Returns customer list when data exists, otherwise returns status false with message.
        /// </response>
        /// <response code="400">
        /// Returns general error message when unexpected error occurs.
        /// </response>
        [HttpPost("list-customer")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GeneralResponse<List<CustomerResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResponse<string>), StatusCodes.Status400BadRequest)]
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
