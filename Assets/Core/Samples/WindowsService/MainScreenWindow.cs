using Core.WindowsService;
using Core.WindowsService.Api.Service;
using UnityEngine;

namespace Core.Samples.WindowsService
{
    public class MainScreenWindow : BaseWindow
    {
        private void OnEnable()
        {
            Debug.LogError("HUI ENABLE");
        }

        private void OnDisable()
        {
            Debug.LogError("HUI DISABLE");
        }

        public override WindowsLayers Layer => WindowsLayers.Window;
    }
}