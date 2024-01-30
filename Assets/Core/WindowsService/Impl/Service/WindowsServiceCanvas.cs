using System.Collections.Generic;
using Core.WindowsService.Api.Service;
using Sirenix.OdinInspector;
using UnityEngine;
using Core.Tools;

namespace Core.WindowsService.Impl.Service
{
    public class WindowsServiceCanvas : SerializedMonoBehaviour, IWindowsServiceCanvas
    {
        public IReadOnlyDictionary<WindowsLayers, Transform> Layers => _layers;

        [ReadOnly] private Dictionary<WindowsLayers, Transform> _layers = new Dictionary<WindowsLayers, Transform>();
        [SerializeField] private Transform _emptyLayer = default;

        private void Awake()
        {
            foreach (var layer in EnumTools.GetElements<WindowsLayers>())
            {
                if (!_layers.ContainsKey(layer))
                {
                    var layerInstance = Instantiate(_emptyLayer, transform);
                    layerInstance.name = layer.ToString();
                    layerInstance.gameObject.SetActive(false);
                    
                    _layers.Add(layer, layerInstance);
                }
            }
        }

    }
}