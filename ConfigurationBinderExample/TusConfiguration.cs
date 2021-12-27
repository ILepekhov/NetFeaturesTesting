using System.Collections.Generic;

namespace ConfigurationBinderExample;

public sealed record TusConfiguration
{
    public UploadDestination Destination { get; init; }

    public Credentials[] Credentials { get; init; }

    public int ChunkSize { get; init; }

    public TimeOnly UploadFrom { get; init; }

    public TimeOnly UploadTo { get; init; }

    public int RetryCount { get; init; }

    public int MaxParallelUploads { get; init; }

    public TusConfiguration()
    {
        Credentials = Array.Empty<Credentials>();
        UploadFrom = new TimeOnly(00, 00, 00);
        UploadTo = new TimeOnly(23, 59, 59);
        RetryCount = 3;
        MaxParallelUploads = 1;
    }
}
