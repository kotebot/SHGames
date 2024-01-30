using System;
using Core.WindowsService.Api.Service;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.Samples.WindowsService
{
    public class UI : MonoBehaviour
    {
        [Inject] private IWindowsService _windowsService;

        private void Start()
        {
        }

        [Button]
        private void ShowMainScreen()
        {
            _windowsService.ShowWindow<MainScreenWindow>();
        }

        [Button]
        private void HideMainScreen()
        {
            _windowsService.HideWindow<MainScreenWindow>();
        }
    }
}