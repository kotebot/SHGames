using Core.Ads.Impl;
using Zenject;

namespace Core.Ads.Zenject
{
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<AdsService>()
                .AsSingle();
        }
    }
}