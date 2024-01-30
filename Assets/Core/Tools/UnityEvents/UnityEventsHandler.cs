using System;
using UnityEngine;

namespace Core.Tools.UnityEvents
{
    public class UnityEventsHandler : MonoBehaviour, IUnityEventsHandler
    {
        public event Action<bool> OnApplicationPauseEvent;
        public event Action OnApplicationQuitEvent;

        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPauseEvent?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Invoke();
        }
    }
}