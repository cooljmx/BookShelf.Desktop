namespace BookShelf.Infrastructure.Settings;

internal class AboutWindowMemento : WindowMemento
{
    public AboutWindowMemento()
    {
        Left = 100;
        Top = 100;
        Width = 600;
        Height = 400;
        IsMaximized = false;
    }
}