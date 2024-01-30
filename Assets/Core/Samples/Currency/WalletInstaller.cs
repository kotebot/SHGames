using Core.Currency.BaseClasses;
using Core.Currency.Data;
using Core.Zenject;
using Zenject;

namespace Core.Samples.Currency
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IWallet<CurrencyType>, IInitializable>()
                .To<MyWallet>()
                .AsSingle();
        }
    }
}