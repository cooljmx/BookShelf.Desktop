using System;
using System.Windows.Input;
using BookShelf.Domain.DevTools;
using BookShelf.Domain.Factories;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.DevTools;
using NLog;

namespace BookShelf.ViewModels.MainWindow;

public class DevToolsMenuViewModel : IDevToolsMenuViewModel
{
    private static readonly ILogger Logger = LogManager.GetLogger(nameof(DevToolsMenuViewModel));
    private readonly IDevToolsStatusProvider _devToolsStatusProvider;
    private readonly IFactory<ILogViewerViewModel> _logViewerViewModelFactory;
    private readonly Command _openLogViewerCommand;

    public DevToolsMenuViewModel(
        IDevToolsStatusProvider devToolsStatusProvider,
        IFactory<ILogViewerViewModel> logViewerViewModelFactory,
        ILogEntryViewModelRepository logEntryViewModelRepository)
    {
        _devToolsStatusProvider = devToolsStatusProvider;
        _logViewerViewModelFactory = logViewerViewModelFactory;

        ThrowExceptionCommand = new Command(() => throw new Exception("Test exception"));
        _openLogViewerCommand = new Command(OpenLogViewer);
        WriteInfoLogCommand = new Command(() => Logger.Info("Testing info log"));
        WriteWarnLogCommand = new Command(() => Logger.Warn("Testing warn log"));
        WriteErrorLogCommand = new Command(() => Logger.Error("Testing error log"));
        WriteFatalLogCommand = new Command(() => Logger.Fatal("Testing fatal log"));
        ClearLogsCommand = new Command(logEntryViewModelRepository.Clear);
    }

    public bool IsVisible => _devToolsStatusProvider.IsEnabled;
    public ICommand ThrowExceptionCommand { get; }
    public ICommand OpenLogViewerCommand => _openLogViewerCommand;
    public ICommand WriteInfoLogCommand { get; }
    public ICommand WriteWarnLogCommand { get; }
    public ICommand WriteErrorLogCommand { get; }
    public ICommand WriteFatalLogCommand { get; }
    public ICommand ClearLogsCommand { get; }
    public event Action<IMainWindowContentViewModel>? ContentViewModelChanged;

    private void OpenLogViewer()
    {
        var logViewerViewModel = _logViewerViewModelFactory.Create();

        ContentViewModelChanged?.Invoke(logViewerViewModel);
    }
}