using System.Collections.Generic;
using UnityEngine;

namespace Core.WindowsService.Api.Service
{
    public interface IWindowsServiceCanvas
    {
        public IReadOnlyDictionary<WindowsLayers, Transform> Layers { get; }
    }
}