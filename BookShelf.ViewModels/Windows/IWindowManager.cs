namespace BookShelf.ViewModels.Windows;

public interface IWindowManager
{
    IWindow<TWindowViewModel> Show<TWindowViewModel>(TWindowViewModel viewModel, bool isModal = false)
        where TWindowViewModel : IWindowViewModel;

    void Close<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel;
}