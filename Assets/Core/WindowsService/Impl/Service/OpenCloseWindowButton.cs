using Core.WindowsService.Api;
using Core.WindowsService.Api.Service;

namespace Core.WindowsService.Impl.Service
{
    public class OpenCloseWindowButton : OpenWindowButton
    {
        private IWindow _window;

        protected override void Start()
        {
            base.Start();
            _window = _windowsService.GetWindow(_windowType.GetType());
        }

        protected override void OnOpenWindow()
        {
            if(!_window.IsOpened)
                _windowsService.ShowWindow(_windowType.GetType());
            else _windowsService.HideWindow(_windowType.GetType());
        }
    }
}