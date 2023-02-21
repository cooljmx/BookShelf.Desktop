using System.Net.Http;
using Autofac;
using BookShelf.Bootstrapper.Factories;
using BookShelf.Domain.Factories;
using BookShelf.Views.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelf.Bootstrapper;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();
        builder.RegisterGeneric(typeof(Factory<>)).As(typeof(IFactory<>)).SingleInstance();
        builder.Register(_ =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddHttpClient()
                    .BuildServiceProvider();

                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

                return httpClientFactory;
            })
            .SingleInstance();
    }
}