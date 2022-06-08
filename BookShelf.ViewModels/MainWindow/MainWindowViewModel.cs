using BookShelf.Domain.Settings;

namespace BookShelf.ViewModels.MainWindow
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        private readonly IMainWindowMementoWrapper _mainWindowMementoWrapper;

        public MainWindowViewModel(IMainWindowMementoWrapper mainWindowMementoWrapper)
        {
            _mainWindowMementoWrapper = mainWindowMementoWrapper;
        }

        public double Left
        {
            get => _mainWindowMementoWrapper.Left;
            set => _mainWindowMementoWrapper.Left = value;
        }

        public double Top
        {
            get => _mainWindowMementoWrapper.Top;
            set => _mainWindowMementoWrapper.Top = value;
        }

        public double Width
        {
            get => _mainWindowMementoWrapper.Width;
            set => _mainWindowMementoWrapper.Width = value;
        }

        public double Height
        {
            get => _mainWindowMementoWrapper.Height;
            set => _mainWindowMementoWrapper.Height = value;
        }

        public bool IsMaximized
        {
            get => _mainWindowMementoWrapper.IsMaximized;
            set => _mainWindowMementoWrapper.IsMaximized = value;
        }

        public string Title => "Book Shelf";
    }
}