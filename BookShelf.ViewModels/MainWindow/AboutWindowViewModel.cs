using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow;

public class AboutWindowViewModel : WindowViewModel<IAboutWindowMementoWrapper>, IAboutWindowViewModel
{
    public AboutWindowViewModel(
        IAboutWindowMementoWrapper windowMementoWrapper,
        IApplicationVersionProvider applicationVersionProvider)
        : base(windowMementoWrapper)
    {
        Version = $"Version {applicationVersionProvider.Version.ToString(3)}";
    }

    public string Version { get; }
}