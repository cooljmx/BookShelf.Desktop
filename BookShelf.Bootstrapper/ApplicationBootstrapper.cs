using System;
using Autofac;
using BookShelf.Bootstrapper.Common;
using BookShelf.Infrastructure.Common;

namespace BookShelf.Bootstrapper;

public class ApplicationBootstrapper : IDisposable
{
    private readonly IContainer _container;

    public ApplicationBootstrapper()
    {
        var containerBuilder = new ContainerBuilder();

        RegisterDependencies(containerBuilder);

        _container = containerBuilder.Build();

        InitializeDependencies();
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IPathServiceInitializer>().Initialize();
    }

    private static void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<Application>().As<IApplication>().SingleInstance();
        containerBuilder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();
    }

    public IApplication CreateApplication()
    {
        return _container.Resolve<IApplication>();
    }

    public void Dispose()
    {
        _container.Dispose();
    }
}