using BookShelf.ViewModels.MainWindow;

namespace BookShelf.Views.MainWindow;

public partial class MainWindow : IMainWindow
{
    public MainWindow(IMainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        DataContext = mainWindowViewModel;
    }
}