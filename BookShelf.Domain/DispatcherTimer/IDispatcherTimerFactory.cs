using System;

namespace BookShelf.Domain.DispatcherTimer;

public interface IDispatcherTimerFactory
{
    IDispatcherTimer Create(TimeSpan interval);
}