using Autofac;
using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Settings;

namespace BookShelf.Infrastructure;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowMementoWrapper>()
            .As<IMainWindowMementoWrapper>()
            .As<IWindowMementoWrapperInitializer>()
            .SingleInstance();

        builder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<AboutWindowMementoWrapper>()
            .As<IAboutWindowMementoWrapper>()
            .As<IWindowMementoWrapperInitializer>()
            .SingleInstance();
    }
}