using Autofac;
using BookShelf.Domain.DispatcherTimer;
using BookShelf.ViewModels.Genres;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.Windows;
using BookShelf.Views.DispatcherTimer;
using BookShelf.Views.Genres;
using BookShelf.Views.Windows;

namespace BookShelf.Views;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindow.MainWindow>().As<IWindow<IMainWindowViewModel>>().InstancePerDependency();
        builder.RegisterType<GenreWindow>().As<IWindow<IGenreWindowViewModel>>().InstancePerDependency();
        builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
        builder.RegisterType<AboutWindow.AboutWindow>().As<IWindow<IAboutWindowViewModel>>()
            .InstancePerDependency();
        builder.RegisterType<DispatcherTimerWrapperFactory>().As<IDispatcherTimerFactory>().SingleInstance();
    }
}