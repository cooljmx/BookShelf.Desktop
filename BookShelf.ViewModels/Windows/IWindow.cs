using System;
using System.ComponentModel;

namespace BookShelf.ViewModels.Windows;

public interface IWindow
{
    void Show();
    void Close();

    event CancelEventHandler Closing;
    event EventHandler Closed;
}