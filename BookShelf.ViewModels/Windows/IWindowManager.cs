namespace BookShelf.ViewModels.Windows;

public interface IWindowManager
{
    IWindow Show<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel;

    void Close<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel;
}