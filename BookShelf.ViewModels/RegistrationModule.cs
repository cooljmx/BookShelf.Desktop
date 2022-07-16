using Autofac;
using BookShelf.ViewModels.MainWindow;

namespace BookShelf.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().InstancePerDependency();
        builder.RegisterType<AboutWindowViewModel>().As<IAboutWindowViewModel>().InstancePerDependency();
    }
}