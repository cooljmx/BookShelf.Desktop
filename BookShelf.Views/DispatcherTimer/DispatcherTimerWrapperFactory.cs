using System;
using BookShelf.Domain.DispatcherTimer;

namespace BookShelf.Views.DispatcherTimer;

internal class DispatcherTimerWrapperFactory : IDispatcherTimerFactory
{
    public IDispatcherTimer Create(TimeSpan interval)
    {
        return new DispatcherTimerWrapper { Interval = interval };
    }
}