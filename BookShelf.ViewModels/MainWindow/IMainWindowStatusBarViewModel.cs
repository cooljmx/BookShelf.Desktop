using System;

namespace BookShelf.ViewModels.MainWindow;

public interface IMainWindowStatusBarViewModel : IDisposable
{
    string Version { get; }
    string CurrentDate { get; }
    string CurrentTime { get; }
}