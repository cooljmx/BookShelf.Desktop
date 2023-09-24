using System;

namespace BookShelf.Domain.Logging;

public class LogArgs
{
    public required DateTime Timestamp { get; init; }
    public required LogLevel LogLevel { get; init; }
    public required string LoggerName { get; init; }
    public required string Message { get; init; }
    public string? StackTrace { get; init; }
}