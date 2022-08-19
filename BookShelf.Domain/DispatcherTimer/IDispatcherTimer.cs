using System;

namespace BookShelf.Domain.DispatcherTimer;

public interface IDispatcherTimer
{
    void Start();
    event EventHandler Tick;
}