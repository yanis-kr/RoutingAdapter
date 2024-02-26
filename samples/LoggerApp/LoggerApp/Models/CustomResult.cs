namespace LoggerApp.Handlers;

public class CustomResult : IResult
{
    private readonly IResult _result;
    private readonly string _correlationId;

    public CustomResult(IResult result, string correlationId)
    {
        _result = result;
        _correlationId = correlationId;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        if (!string.IsNullOrEmpty(_correlationId))
        {
            httpContext.Response.Headers[Constants.CorrelationIdName] = _correlationId;
        }

        await _result.ExecuteAsync(httpContext);
    }
}
