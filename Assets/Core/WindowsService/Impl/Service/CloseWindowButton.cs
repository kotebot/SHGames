using Core.WindowsService.Api;
using Core.WindowsService.Api.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.WindowsService.Impl.Service
{
    [RequireComponent(typeof(Button))]
    public class CloseWindowButton: MonoBehaviour
    {
        [Inject] protected IWindowsService _windowsService;

        [SerializeField] protected BaseWindow _windowType;

        private Button _button;

        protected virtual void Start()
        {
            _button = GetComponent<Button>();
        
            _button.onClick.AddListener(OnOpenBuilderModeWindow);
        }

        protected virtual void OnOpenBuilderModeWindow()
        {
            _windowsService.HideWindow(_windowType.GetType());
        }
    }
}