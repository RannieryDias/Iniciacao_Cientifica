namespace LegisVisao.Exceptions
{
    public class ApiException : Exception
    {
        public int? StatusCode { get; }
        public string? ResponseBody { get; }

        public ApiException(string message, int? statusCode = null, string? responseBody = null)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }

        public ApiException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
