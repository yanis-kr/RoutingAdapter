using System.Text;
using Microsoft.IO;
using Serilog;
//dotnet add package Microsoft.IO.RecyclableMemoryStream
//dotnet add package Serilog.Enrichers.Environment
//builder.Host.UseSerilog((ctx, lc) => lc
//    .WriteTo.Console()
//    .ReadFrom.Configuration(ctx.Configuration)
//    .Enrich.FromLogContext()
//    .Enrich.WithEnvironmentName()
//    .Enrich.WithThreadId());

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequest(context);
        await LogResponse(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();

        await using var requestStream = _recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);
        Log.Information($"{Environment.NewLine}**HTTP Request Information:{Environment.NewLine}" +
                        $"**Headers: {Environment.NewLine} {GetHeadersText(context.Request.Headers)}" +
                        $"**Body: {Environment.NewLine} {ReadStreamInChunks(requestStream)} ");
        context.Request.Body.Position = 0;
    }

    private static string GetHeadersText(IHeaderDictionary headers)
    {
        var sb = new StringBuilder();
        foreach (var (key, value) in headers)
        {
            sb.Append($"{key}: {value}{Environment.NewLine}");
        }
        return sb.ToString();
    }

    private async Task LogResponse(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        await using var responseBody = _recyclableMemoryStreamManager.GetStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        Log.Information($"{Environment.NewLine}**HTTP Response Information:{Environment.NewLine}" +
                        $"**Headers:{Environment.NewLine} {GetHeadersText(context.Response.Headers)}" +
                        $"**Body: {Environment.NewLine} {text}");

        // Copy the contents of the new body to the original body
        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;
        stream.Seek(0, SeekOrigin.Begin);
        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);
        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;
        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);
        return textWriter.ToString();
    }
}
