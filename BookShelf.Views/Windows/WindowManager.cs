using System;
using System.Collections.Generic;
using System.ComponentModel;
using BookShelf.ViewModels.Windows;
using BookShelf.Views.Factories;

namespace BookShelf.Views.Windows;

internal class WindowManager : IWindowManager
{
    private readonly Dictionary<IWindowViewModel, IWindow> _viewModelToWindowMap = new();
    private readonly IWindowFactory _windowFactory;
    private readonly Dictionary<IWindow, IWindowViewModel> _windowToViewModelMap = new();

    public WindowManager(IWindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    public IWindow Show<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        var window = _windowFactory.Create(viewModel);

        _viewModelToWindowMap.Add(viewModel, window);
        _windowToViewModelMap.Add(window, viewModel);

        window.Closing += OnWindowClosing;
        window.Closed += OnWindowClosed;

        window.Show();

        return window;
    }

    public void Close<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
            window.Close();
    }

    private void OnWindowClosed(object sender, EventArgs e)
    {
        if (sender is IWindow window && _windowToViewModelMap.TryGetValue(window, out var viewModel))
        {
            window.Closing -= OnWindowClosing;
            window.Closed -= OnWindowClosed;

            _viewModelToWindowMap.Remove(viewModel);
            _windowToViewModelMap.Remove(window);
        }
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        if (sender is IWindow window && _windowToViewModelMap.TryGetValue(window, out var viewModel))
            viewModel.WindowClosing();
    }
}