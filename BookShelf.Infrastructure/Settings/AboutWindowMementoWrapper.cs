using BookShelf.Domain.Settings;
using BookShelf.Infrastructure.Common;

namespace BookShelf.Infrastructure.Settings
{
    internal class AboutWindowMementoWrapper : WindowMementoWrapper<AboutWindowMemento>, IAboutWindowMementoWrapper
    {
        public AboutWindowMementoWrapper(IPathService pathService)
            : base(pathService)
        {
        }

        protected override string MementoName => "AboutWindowMemento";
    }
}