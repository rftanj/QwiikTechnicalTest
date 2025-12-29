namespace QwiikTechnicalTest.Models.DTO.Customer
{
    /// <summary>
    /// Request filter for retrieving customer data.
    /// </summary>
    public class ListCustomerRequest
    {
        /// <summary>
        /// Customer ID to filter the result.
        /// Optional.
        /// </summary>
        /// <example>123</example>
        public string? customer_id { get; set; }
    }
}
