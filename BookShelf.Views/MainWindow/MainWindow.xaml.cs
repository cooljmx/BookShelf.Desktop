using BookShelf.ViewModels.MainWindow;

namespace BookShelf.Views.MainWindow;

public partial class MainWindow
{
    public MainWindow(IMainWindowViewModel mainWindowViewModel)
        : base(mainWindowViewModel)
    {
        InitializeComponent();
    }
}