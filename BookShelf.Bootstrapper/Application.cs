﻿using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using BookShelf.Domain.Factories;
using BookShelf.Infrastructure.Settings;
using BookShelf.ViewModels.DevTools;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.Themes;
using BookShelf.ViewModels.Windows;
using NLog;

namespace BookShelf.Bootstrapper;

internal class Application : IApplication, IDisposable
{
    private static readonly ILogger Logger = LogManager.GetLogger(nameof(Application));
    private readonly ILifetimeScope _applicationLifetimeScope;
    private IMainWindowViewModel? _mainWindowViewModel;

    public Application(ILifetimeScope lifetimeScope)
    {
        Logger.Info("Created");

        _applicationLifetimeScope = lifetimeScope.BeginLifetimeScope(RegisterDependencies);
    }

    public Window Run()
    {
        InitializeDependencies();

        var mainWindowViewModelFactory = _applicationLifetimeScope.Resolve<IFactory<IMainWindowViewModel>>();
        _mainWindowViewModel = mainWindowViewModelFactory.Create();
        var windowManager = _applicationLifetimeScope.Resolve<IWindowManager>();

        var mainWindow = windowManager.Show(_mainWindowViewModel);

        if (mainWindow is not Window window)
            throw new NotImplementedException();

        return window;
    }

    private static void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder
            .RegisterModule<Infrastructure.RegistrationModule>()
            .RegisterModule<ViewModels.RegistrationModule>()
            .RegisterModule<Views.RegistrationModule>()
            .RegisterModule<RegistrationModule>();
    }

    private void InitializeDependencies()
    {
        var windowMementoWrapperInitializers =
            _applicationLifetimeScope.Resolve<IEnumerable<IWindowMementoWrapperInitializer>>();

        foreach (var windowMementoWrapperInitializer in windowMementoWrapperInitializers)
            windowMementoWrapperInitializer.Initialize();

        _applicationLifetimeScope.Resolve<ILogEntryViewModelRepository>();
        _applicationLifetimeScope.Resolve<IThemeManager>().SwitchTo(Theme.Dark);
    }

    public void Dispose()
    {
        _mainWindowViewModel?.Dispose();
        _applicationLifetimeScope.Dispose();

        Logger.Info("Disposed");
    }
}