namespace ConfigurationBinderExample;

public sealed record Credentials
{
    public UploadDestination Destination { get; init; }
    public string? Url { get; init; }
    public string? UploadToken { get; init; }
    public string? ClientCode { get; init; }
}
