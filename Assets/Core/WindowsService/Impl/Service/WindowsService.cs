using System;
using System.Collections.Generic;
using System.Linq;
using Core.WindowsService.Api.Repository;
using Core.WindowsService.Api.Service;
using Core.WindowsService.Data;
using Zenject;
using Core.Zenject;
using Object = UnityEngine.Object;

namespace Core.WindowsService.Impl.Service
{
    public class WindowsService : IWindowsService, IInitializable
    {
        [Inject] private IWindowsRepository _repository;
        [Inject] private DiContainer _diContainer;
        
        public event Action<IWindow> OnWindowOpen;
        public event Action<IWindow> OnWindowHide;
        
        public bool AnyWindowOpen => _spawnedWindows.Any(win => win.IsOpened);
        
        private List<IWindow> _spawnedWindows = new List<IWindow>();
        private IWindow[] _windowsPrefabs;

        private IWindowsServiceCanvas _canvas;

        public void Initialize()
        {
            _windowsPrefabs = _repository.GetWindowsPrefabs().ToArray();
            _canvas = Object.FindAnyObjectByType<WindowsServiceCanvas>();
        }

        public void ShowWindow<TType>(WindowsOption option = null) where TType : IWindow
        { 
            var window = GetWindow<TType>();

            ShowWindowInternal(window, option);
        }
        
        public void ShowWindow(Type type, WindowsOption option = null)
        {
            var window = GetWindow(type);

            ShowWindowInternal(window, option);
        }

        public void HideWindow(Type type)
        {
            var window = GetWindow(type);
            
            HideWindowInternal(window);
        }

        public void HideWindow<TType>() where TType : IWindow
        {
            var window = GetWindow<TType>();

            HideWindowInternal(window);
        }

        public void HideAllWindows()
        {
            foreach (var window in _spawnedWindows)
            {
                window.Hide();
                OnWindowHide?.Invoke(window);
            }
        }
        
        public IWindow GetWindow<TType>() where TType : IWindow
        {
            var window = _spawnedWindows.FirstOrDefault(window => window is TType);
            if (window == default)
            {
                return SpawnWindow<TType>();
            }

            return window;
        }
        
        public IWindow GetWindow(Type type)
        {
            var window = _spawnedWindows.FirstOrDefault(window => window.GetType() == type);
            
            if (window == default)
            {
                return SpawnWindow(type);
            }
            
            return window;
        }
        
        private void ShowWindowInternal(IWindow window, WindowsOption option = null) 
        {
            if (window != default)
            {
                _canvas.Layers[window.Layer].gameObject.SetActive(true);
                
                window.Show(option);
                OnWindowOpen?.Invoke(window);
            }
        }
        
        private void HideWindowInternal(IWindow window) 
        {
            if (window != default)
            {
                _canvas.Layers[window.Layer].gameObject.SetActive(false);

                window.Hide();
                OnWindowHide?.Invoke(window);
            }
        }

        private BaseWindow SpawnWindow(Type type)
        {
            var prefab = _windowsPrefabs.FirstOrDefault(window => window.GetType() == type);

            if (prefab == default)
                throw new NullReferenceException($"{type} is missing. Add {type} prefab to Resources/Windows.");
            
            if(_canvas == default)
                throw new NullReferenceException($"{_canvas} is missing. Add {nameof(WindowsServiceCanvas)} component to windows canvas.");
            
            var instance = _diContainer.Instantiate((prefab as BaseWindow), _canvas.Layers[prefab.Layer].transform);
            
            _spawnedWindows.Add(instance);

            return instance;
        }

        private BaseWindow SpawnWindow<TType>() where TType : IWindow
        {
            var prefab = _windowsPrefabs.FirstOrDefault(window => window is TType);
            
            if (prefab == default)
                throw new NullReferenceException($"{typeof(TType)} is missing. Add {typeof(TType)} prefab to Resources/Windows.");
            
            if(_canvas == default)
                throw new NullReferenceException($"{_canvas} is missing. Add {nameof(WindowsServiceCanvas)} component to windows canvas.");
            
            var instance = _diContainer.Instantiate((prefab as BaseWindow), _canvas.Layers[prefab.Layer].transform);
            
            _spawnedWindows.Add(instance);

            return instance;
        }
    }
}