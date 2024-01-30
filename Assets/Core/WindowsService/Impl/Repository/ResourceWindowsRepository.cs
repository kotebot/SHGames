using System.Collections.Generic;
using System.Linq;
using Core.WindowsService.Api.Repository;
using Core.WindowsService.Api.Service;
using UnityEngine;

namespace Core.WindowsService.Impl.Repository
{
    public class ResourceWindowsRepository : IWindowsRepository
    {
        private const string Path = "Windows";
        public IEnumerable<IWindow> GetWindowsPrefabs()
        {
            var windows = Resources.LoadAll<GameObject>(Path);
            return windows.Select(w => w.GetComponent<IWindow>());
        }
    }
}