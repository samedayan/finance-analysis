using System.Net;

namespace Finance.Analysis.Infrastructure.Loggers;

public interface IConsoleLogger
{
    Task LogTrace(string message, Exception? exception = null, string? responseBody = null,
        string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null,
        long? duration = null, string? hostName = null, string? url = null, string? origin = null,
        Guid? correlationId = null);

    Task LogDebug(string message, Exception? exception = null, string? responseBody = null, string? requestBody = null,
        HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
        string? hostName = null, string? url = null, string? origin = null, Guid? correlationId = null);

    Task LogInformation(string message, Exception? exception = null, string? responseBody = null,
        string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null,
        long? duration = null, string? hostName = null, string? url = null,
        string? origin = null, Guid? correlationId = null);

    Task LogWarning(string message, Exception? exception = null, string? responseBody = null,
        string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null,
        long? duration = null, string? hostName = null, string? url = null,
        string? origin = null, Guid? correlationId = null);

    Task LogError(string message, Exception? exception = null, string? responseBody = null, string? requestBody = null,
        HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null, long? duration = null,
        string? hostName = null, string? url = null, string? origin = null, Guid? correlationId = null);

    Task LogCritical(string message, Exception? exception = null, string? responseBody = null,
        string? requestBody = null, HttpMethod? httpMethod = null, HttpStatusCode? httpStatusCode = null,
        long? duration = null, string? hostName = null, string? url = null, string? origin = null,
        Guid? correlationId = null);
}