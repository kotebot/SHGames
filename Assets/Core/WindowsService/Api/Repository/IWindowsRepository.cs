using System.Collections.Generic;
using Core.WindowsService.Api.Service;

namespace Core.WindowsService.Api.Repository
{
    public interface IWindowsRepository
    {
        public IEnumerable<IWindow> GetWindowsPrefabs();
    }
}