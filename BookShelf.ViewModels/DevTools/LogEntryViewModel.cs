﻿using System;
using BookShelf.Domain.Logging;

namespace BookShelf.ViewModels.DevTools;

public class LogEntryViewModel
{
    public required DateTime Timestamp { get; init; }
    public required string TimestampValue { get; init; }
    public required LogLevel Level { get; init; }
    public required string LoggerName { get; init; }
    public required string Message { get; init; }
    public string? StackTrace { get; init; }
    public bool IsStackTraceVisible => StackTrace is not null;
}