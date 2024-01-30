using Core.Currency.BaseClasses;
using Core.Currency.Data;
using Core.Zenject;
using Zenject;

namespace Core.Currency.Zenject
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BaseWallet>()
                .AsSingle();
        }
    }
}