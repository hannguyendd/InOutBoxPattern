using System.Net;

namespace Shared.Contracts;

public record ApiErrorResponse(HttpStatusCode Status, string Message, DateTime Timestamp, string Error, Guid TraceId);