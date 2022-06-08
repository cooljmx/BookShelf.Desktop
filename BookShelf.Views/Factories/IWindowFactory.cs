using BookShelf.ViewModels.Windows;

namespace BookShelf.Views.Factories;

public interface IWindowFactory
{
    IWindow<TWindowViewModel> Create<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel;
}