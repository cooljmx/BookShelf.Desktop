using System;
using System.Collections.Generic;
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

    public IWindow<TWindowViewModel> Show<TWindowViewModel>(TWindowViewModel viewModel, bool isModal = false)
        where TWindowViewModel : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
        {
            window.Activate();

            return (IWindow<TWindowViewModel>)window;
        }

        window = _windowFactory.Create(viewModel);

        _viewModelToWindowMap.Add(viewModel, window);
        _windowToViewModelMap.Add(window, viewModel);

        window.Closed += OnWindowClosed;

        viewModel.BeforeWindowShown();

        if (isModal)
            window.ShowDialog();
        else
            window.Show();

        return (IWindow<TWindowViewModel>)window;
    }

    public void Close<TWindowViewModel>(TWindowViewModel viewModel)
        where TWindowViewModel : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
            window.Close();
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        if (sender is not IWindow<IWindowViewModel> window)
            return;

        window.Closed -= OnWindowClosed;

        if (!_windowToViewModelMap.TryGetValue(window, out var viewModel))
            return;

        viewModel.AfterWindowClosed();

        _windowToViewModelMap.Remove(window);
        _viewModelToWindowMap.Remove(viewModel);
    }
}