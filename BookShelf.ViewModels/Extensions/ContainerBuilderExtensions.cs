using Autofac;

namespace BookShelf.ViewModels.Extensions;

public static class ContainerBuilderExtensions
{
    public static void RegisterViewModel<TImplementation, TInterface>(this ContainerBuilder builder)
        where TInterface : notnull
        where TImplementation : TInterface
    {
        builder.RegisterType<TImplementation>().As<TInterface>().InstancePerDependency().ExternallyOwned();
    }
}