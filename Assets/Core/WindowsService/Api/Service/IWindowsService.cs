using System;
using Core.WindowsService.Data;

namespace Core.WindowsService.Api.Service
{
    public interface IWindowsService
    {
        public event Action<IWindow> OnWindowOpen;
        public event Action<IWindow> OnWindowHide;
        public bool AnyWindowOpen { get; }

        public void ShowWindow<TType>(WindowsOption option = null) where TType : IWindow;
        public void HideWindow<TType>() where TType : IWindow;

        public void ShowWindow(Type type, WindowsOption option = null);
        public void HideWindow(Type type);
        public IWindow GetWindow<TType>() where TType : IWindow;
        public IWindow GetWindow(Type type);
        public void HideAllWindows();
    }
}