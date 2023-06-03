using System;
using System.IO;
using BookShelf.Infrastructure.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace BookShelf.Bootstrapper.Logging;

internal class LogManagerInitializer : ILogManagerInitializer, IDisposable
{
    private readonly IPathService _pathService;

    public LogManagerInitializer(IPathService pathService)
    {
        _pathService = pathService;

        var loggingConfiguration = CreateLoggingConfiguration();

        LogManager.Configuration = loggingConfiguration;
    }

    private LoggingConfiguration CreateLoggingConfiguration()
    {
        var loggingConfiguration = new LoggingConfiguration();

        var appLoggingRule = CreateAppLoggingRule();
        loggingConfiguration.AddRule(appLoggingRule);

        return loggingConfiguration;
    }

    private LoggingRule CreateAppLoggingRule()
    {
        var appLogFileTarget = new FileTarget
        {
            FileName = Path.Combine(_pathService.ApplicationFolder, "Logs", "app.log")
        };

        var asyncTargetWrapper = new AsyncTargetWrapper(appLogFileTarget)
        {
            TimeToSleepBetweenBatches = 1000
        };

        var loggingRule = new LoggingRule("*", LogLevel.Info, asyncTargetWrapper);

        return loggingRule;
    }

    public void Dispose()
    {
        LogManager.Flush();
        LogManager.Shutdown();
    }
}