using Autofac;
using BookShelf.Domain.Rest;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Rest;
using BookShelf.Infrastructure.Settings;
using BookShelf.Infrastructure.Version;

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

        builder.RegisterType<ApplicationVersionProvider>().As<IApplicationVersionProvider>().SingleInstance();
        builder.RegisterType<ApiRequestExecutor>().As<IApiRequestExecutor>().SingleInstance();
    }
}