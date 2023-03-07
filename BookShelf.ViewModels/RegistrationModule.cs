using Autofac;
using BookShelf.ViewModels.Authors;
using BookShelf.ViewModels.Extensions;
using BookShelf.ViewModels.MainWindow;

namespace BookShelf.ViewModels;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterViewModel<MainWindowViewModel, IMainWindowViewModel>();
        builder.RegisterViewModel<AboutWindowViewModel, IAboutWindowViewModel>();
        builder.RegisterViewModel<AuthorCollectionViewModel, IAuthorCollectionViewModel>();
    }
}