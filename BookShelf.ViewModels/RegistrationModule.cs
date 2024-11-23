using Autofac;
using BookShelf.ViewModels.Authors;
using BookShelf.ViewModels.DevTools;
using BookShelf.ViewModels.Extensions;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.MainWindow.Menu;

namespace BookShelf.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterViewModel<MainWindowViewModel, IMainWindowViewModel>();
        builder.RegisterViewModel<AboutWindowViewModel, IAboutWindowViewModel>();
        builder.RegisterViewModel<AuthorCollectionViewModel, IAuthorCollectionViewModel>();
        builder.RegisterViewModel<MainWindowMenuViewModel, IMainWindowMenuViewModel>();
        builder.RegisterViewModel<MainWindowStatusBarViewModel, IMainWindowStatusBarViewModel>();
        builder.RegisterViewModel<DevToolsMenuViewModel, IDevToolsMenuViewModel>();
        builder.RegisterViewModel<LogViewerViewModel, ILogViewerViewModel>();
        builder.RegisterViewModel<ThemesMenuViewModel, IThemesMenuViewModel>();
        builder.RegisterViewModel<ViewMenuViewModel, IViewMenuViewModel>();

        builder.RegisterType<LogEntryViewModelRepository>().As<ILogEntryViewModelRepository>().SingleInstance();
    }
}