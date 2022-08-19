using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Settings;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.Windows;

namespace BookShelf.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;
    private IMainWindowViewModel _mainWindowViewModel;

    public Bootstrapper()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder
            .RegisterModule<Infrastructure.RegistrationModule>()
            .RegisterModule<ViewModels.RegistrationModule>()
            .RegisterModule<Views.RegistrationModule>()
            .RegisterModule<RegistrationModule>();

        _container = containerBuilder.Build();
    }

    public Window Run()
    {
        InitializeDependencies();

        _mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();
        var windowManager = _container.Resolve<IWindowManager>();

        var mainWindow = windowManager.Show(_mainWindowViewModel);

        if (mainWindow is not Window window)
            throw new NotImplementedException();

        return window;
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IPathServiceInitializer>().Initialize();

        var windowMementoWrapperInitializers = _container.Resolve<IEnumerable<IWindowMementoWrapperInitializer>>();

        foreach (var windowMementoWrapperInitializer in windowMementoWrapperInitializers)
            windowMementoWrapperInitializer.Initialize();
    }

    public void Dispose()
    {
        _mainWindowViewModel.Dispose();
        _container.Dispose();
    }
}