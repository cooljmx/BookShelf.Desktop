using System.Windows;
using BookShelf.ViewModels.Windows;

namespace BookShelf.Views.Windows;

public abstract class BaseWindow<TWindowViewModel> : Window, IWindow<TWindowViewModel>
    where TWindowViewModel : IWindowViewModel
{
    protected BaseWindow(TWindowViewModel viewModel)
    {
        DataContext = viewModel;
    }
}