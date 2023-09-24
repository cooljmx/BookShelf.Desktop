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
    private readonly ILogNotifier _logNotifier;
    private readonly IPathService _pathService;

    public LogManagerInitializer(
        IPathService pathService,
        ILogNotifier logNotifier)
    {
        _pathService = pathService;
        _logNotifier = logNotifier;

        var loggingConfiguration = CreateLoggingConfiguration();

        LogManager.Configuration = loggingConfiguration;
    }

    private LoggingConfiguration CreateLoggingConfiguration()
    {
        var loggingConfiguration = new LoggingConfiguration();

        var appLoggingRule = CreateAppLoggingRule();
        loggingConfiguration.AddRule(appLoggingRule);

        var notificationLoggingRule = CreateNotificationLoggingRule();
        loggingConfiguration.AddRule(notificationLoggingRule);

        return loggingConfiguration;
    }

    private LoggingRule CreateNotificationLoggingRule()
    {
        var notificationTarget = new NotificationTarget(_logNotifier);

        var notificationLoggingRule = new LoggingRule("*", LogLevel.Info, notificationTarget);

        return notificationLoggingRule;
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