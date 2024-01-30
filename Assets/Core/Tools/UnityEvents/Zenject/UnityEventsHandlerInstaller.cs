using UnityEngine;
using Zenject;

namespace Core.Tools.UnityEvents.Zenject
{
    public class UnityEventsHandlerInstaller : MonoInstaller
    {
        [SerializeField] private UnityEventsHandler _eventsHandler;

        public override void InstallBindings()
        {
            Container
                .Bind<IUnityEventsHandler>()
                .FromInstance(_eventsHandler)
                .AsSingle();
        }
    }
}