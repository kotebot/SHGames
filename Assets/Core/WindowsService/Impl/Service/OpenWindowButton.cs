using Core.WindowsService.Api;
using Core.WindowsService.Api.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.WindowsService.Impl.Service
{
    [RequireComponent(typeof(Button))]
    public class OpenWindowButton : MonoBehaviour
    {
        [Inject] protected IWindowsService _windowsService;

        [SerializeField] protected BaseWindow _windowType;

        private Button _button;

        protected virtual void Start()
        {
            _button = GetComponent<Button>();
        
            _button.onClick.AddListener(OnOpenWindow);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnOpenWindow);
        }

        protected virtual void OnOpenWindow()
        {
            _windowsService.ShowWindow(_windowType.GetType());
        }
    }
}