using System;
using System.Windows;
using Autofac;
using BookShelf.Infrastructure.Settings;
using BookShelf.ViewModels.MainWindow;
using BookShelf.ViewModels.Windows;

namespace BookShelf.Bootstrapper
{
    public class Bootstrapper : IDisposable
    {
        private readonly IContainer _container;

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

        public void Dispose()
        {
            _container.Dispose();
        }

        public Window Run()
        {
            InitializeDependencies();

            var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();
            var windowManager = _container.Resolve<IWindowManager>();

            var mainWindow = windowManager.Show(mainWindowViewModel);

            if (mainWindow is not Window window)
                throw new NotImplementedException();

            return window;
        }

        private void InitializeDependencies()
        {
            _container.Resolve<IMainWindowMementoWrapperInitializer>().Initialize();
        }
    }
}