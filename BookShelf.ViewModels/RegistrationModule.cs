using Autofac;
using BookShelf.ViewModels.Genres;
using BookShelf.ViewModels.MainWindow;

namespace BookShelf.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().InstancePerDependency()
            .ExternallyOwned();
        builder.RegisterType<AboutWindowViewModel>().As<IAboutWindowViewModel>().InstancePerDependency()
            .ExternallyOwned();
        builder.RegisterType<GenreCollectionViewModel>().As<IGenreCollectionViewModel>().InstancePerDependency()
            .ExternallyOwned();
        builder.RegisterType<GenreCollectionItemViewModel>().As<IGenreCollectionItemViewModel>()
            .InstancePerDependency().ExternallyOwned();
        builder.RegisterType<GenreWindowViewModel>().As<IGenreWindowViewModel>().InstancePerDependency()
            .ExternallyOwned();
    }
}