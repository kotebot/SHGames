using Core.WindowsService.Data;
using UnityEngine;

namespace Core.WindowsService.Api.Service
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public bool IsOpened => gameObject.activeSelf;
        public abstract WindowsLayers Layer { get; }
        
        protected WindowsOption _option;
        
        public virtual void Show(WindowsOption option = null)
        {
            _option = option;
            
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            
            _option = null;
        }
    }
}