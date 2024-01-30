
using Core.WindowsService.Data;

namespace Core.WindowsService.Api.Service
{
    public interface IWindow
    {
        public bool IsOpened { get; }
        public WindowsLayers Layer { get; }

        public void Show(WindowsOption option = null);
        public void Hide();
    }
}