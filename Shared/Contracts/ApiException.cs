using System.Net;

namespace Shared.Contracts
{
    public class ApiException(HttpStatusCode status, string message) : Exception(message)
    {
        public HttpStatusCode Status { get; set; } = status;
        public ApiException(HttpStatusCode status) : this(status, "An error occurred.") { }
    }
}