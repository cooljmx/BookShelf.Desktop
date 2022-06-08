using Autofac;
using BookShelf.Domain.Genres;
using BookShelf.Domain.Settings;
using BookShelf.Domain.Version;
using BookShelf.Infrastructure.Common;
using BookShelf.Infrastructure.Genres;
using BookShelf.Infrastructure.Serialization;
using BookShelf.Infrastructure.Settings;
using BookShelf.Infrastructure.Storage;
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

        builder.RegisterType<GenreWindowMementoWrapper>()
            .As<IGenreWindowMementoWrapper>()
            .As<IWindowMementoWrapperInitializer>()
            .SingleInstance();

        builder.RegisterType<JsonService>().As<IJsonService>().SingleInstance();

        builder.RegisterType<GenreDtoStorage>()
            .As<IGenreDtoStorage>()
            .As<IGenreDtoStorageInitializer>()
            .SingleInstance();

        builder.RegisterType<GenreTranslator>().As<IGenreTranslator>().SingleInstance();
        builder.RegisterType<GenreRepository>().As<IGenreRepository>().SingleInstance();

        builder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<GenreFactory>().As<IGenreFactory>().SingleInstance();

        builder.RegisterType<AboutWindowMementoWrapper>()
            .As<IAboutWindowMementoWrapper>()
            .As<IWindowMementoWrapperInitializer>()
            .SingleInstance();

        builder.RegisterType<ApplicationVersionProvider>().As<IApplicationVersionProvider>().SingleInstance();
    }
}