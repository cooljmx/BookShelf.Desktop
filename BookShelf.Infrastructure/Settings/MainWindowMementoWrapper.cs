using System;
using System.IO;
using BookShelf.Domain.Settings;
using Newtonsoft.Json;

namespace BookShelf.Infrastructure.Settings
{
    internal class MainWindowMementoWrapper : IMainWindowMementoWrapper, IMainWindowMementoWrapperInitializer,
        IDisposable
    {
        private bool _initialized;
        private MainWindowMemento _mainWindowMemento;
        private string _settingsFilePath;

        public MainWindowMementoWrapper()
        {
            _mainWindowMemento = new MainWindowMemento();
        }

        public void Dispose()
        {
            EnsureInitialized();

            var serializedMemento = JsonConvert.SerializeObject(_mainWindowMemento);
            File.WriteAllText(_settingsFilePath, serializedMemento);
        }

        public double Left
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Left;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Left = value;
            }
        }

        public double Top
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Top;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Top = value;
            }
        }

        public double Width
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Width;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Width = value;
            }
        }

        public double Height
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.Height;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.Height = value;
            }
        }

        public bool IsMaximized
        {
            get
            {
                EnsureInitialized();
                return _mainWindowMemento.IsMaximized;
            }
            set
            {
                EnsureInitialized();
                _mainWindowMemento.IsMaximized = value;
            }
        }

        public void Initialize()
        {
            if (_initialized)
                throw new InvalidOperationException($"{nameof(IMainWindowMementoWrapper)} is already initialized");

            _initialized = true;

            var localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            const string company = "DevTricks";
            const string applicationName = "BookShelf";
            const string settingsFolderName = "Settings";

            var settingsPath = Path.Combine(localApplicationDataPath, company, applicationName, settingsFolderName);
            _settingsFilePath = Path.Combine(settingsPath, "MainWindowMemento.json");

            Directory.CreateDirectory(settingsPath);

            if (!File.Exists(_settingsFilePath))
                return;

            var serializedMemento = File.ReadAllText(_settingsFilePath);

            _mainWindowMemento = JsonConvert.DeserializeObject<MainWindowMemento>(serializedMemento);
        }

        private void EnsureInitialized()
        {
            if (!_initialized)
                throw new InvalidOperationException($"{nameof(IMainWindowMementoWrapper)} is not initialized");
        }
    }
}