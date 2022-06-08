using Autofac;
using BookShelf.ViewModels.Windows;
using BookShelf.Views.Factories;

namespace BookShelf.Bootstrapper.Factories;

internal class WindowFactory : IWindowFactory
{
    private readonly IComponentContext _componentContext;

    public WindowFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public IWindow<TWindowViewModel> Create<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        var typedParameter = TypedParameter.From(viewModel);

        var instance = _componentContext.Resolve<IWindow<TWindowViewModel>>(typedParameter);

        return instance;
    }
}