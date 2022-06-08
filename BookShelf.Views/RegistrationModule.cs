using Autofac;
using BookShelf.ViewModels.Windows;
using BookShelf.Views.MainWindow;
using BookShelf.Views.Windows;

namespace BookShelf.Views
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
        }
    }
}