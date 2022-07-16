using Autofac;
using BookShelf.Bootstrapper.Factories;
using BookShelf.Views.Factories;

namespace BookShelf.Bootstrapper;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();
    }
}