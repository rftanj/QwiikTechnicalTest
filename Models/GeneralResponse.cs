namespace QwiikTechnicalTest.Models
{
    public class GeneralResponse<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public static GeneralResponse<T> Success(T data, string message = "Success")
        {
            return new GeneralResponse<T>
            {
                status = true,
                message = message,
                data = data
            };
        }

        public static GeneralResponse<T> Fail(string message)
        {
            return new GeneralResponse<T>
            {
                status = false,
                message = message,
                data = default
            };
        }
    }
}
