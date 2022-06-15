using System.Windows.Input;
using BookShelf.Domain.Settings;
using BookShelf.ViewModels.Commands;
using BookShelf.ViewModels.Windows;

namespace BookShelf.ViewModels.MainWindow
{
    public class MainWindowViewModel : WindowViewModel<IMainWindowMementoWrapper>, IMainWindowViewModel
    {
        private readonly IAboutWindowViewModel _aboutWindowViewModel;
        private readonly Command _closeMainWindowCommand;
        private readonly IWindowManager _windowManager;
        private readonly Command _openAboutWindowCommand;

        public MainWindowViewModel(
            IMainWindowMementoWrapper mainWindowMementoWrapper,
            IWindowManager windowManager,
            IAboutWindowViewModel aboutWindowViewModel)
            : base(mainWindowMementoWrapper)
        {
            _windowManager = windowManager;
            _aboutWindowViewModel = aboutWindowViewModel;

            _closeMainWindowCommand = new Command(CloseMainWindow);
            _openAboutWindowCommand = new Command(OpenAboutWindow);
        }

        public string Title => "Book Shelf";

        public ICommand CloseMainWindowCommand => _closeMainWindowCommand;
        public ICommand OpenAboutWindowCommand => _openAboutWindowCommand;

        private void OpenAboutWindow()
        {
            _windowManager.Show(_aboutWindowViewModel);
        }

        private void CloseMainWindow()
        {
            _windowManager.Close(this);
        }
    }
}