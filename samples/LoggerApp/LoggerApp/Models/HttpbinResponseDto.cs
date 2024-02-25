namespace LoggerApp.Models;

public class HttpbinResponseDto
{
    public Dictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
    public string? Data { get; set; }
    public Dictionary<string, object> Files { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, object> Form { get; set; } = new Dictionary<string, object>();
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public object? Json { get; set; } // 'object' type if the structure of Json is unknown or dynamic
    public string? Method { get; set; }
    public string? Origin { get; set; }
    public string? Url { get; set; }
}
