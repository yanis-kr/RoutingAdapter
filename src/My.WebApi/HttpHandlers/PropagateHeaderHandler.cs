namespace My.WebApi.HttpHandlers;

public class PropagateHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PropagateHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Try to get the header value from the current request
        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(HeaderConstants.HeaderCorrelationId, out var correlationId))
        {
            // If the header exists, add it to the outgoing request
            request.Headers.Add(HeaderConstants.HeaderCorrelationId, (string?)correlationId);
        }

        // Optionally, add the Authorization header from the current request or use another method to set it
        var authorization = (string?)_httpContextAccessor.HttpContext.Request.Headers[HeaderConstants.HeaderAuthorization];
        if (!string.IsNullOrEmpty(authorization))
        {
            request.Headers.TryAddWithoutValidation(HeaderConstants.HeaderAuthorization, authorization);
        }

        // Proceed with the handler chain
        return await base.SendAsync(request, cancellationToken);
    }
}

