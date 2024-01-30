using Core.WindowsService.Api.Repository;
using Core.WindowsService.Impl.Repository;
using Zenject;

namespace Core.WindowsService.Zenject
{
    public class WindowsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IWindowsRepository>()
                .To<ResourceWindowsRepository>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesTo<Impl.Service.WindowsService>()
                .FromNew()
                .AsSingle();
        }
    }
}