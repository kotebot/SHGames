using System;

namespace Core.Tools.UnityEvents
{
    public interface IUnityEventsHandler
    {
        public event Action<bool> OnApplicationPauseEvent;
        public event Action OnApplicationQuitEvent;
    }
}