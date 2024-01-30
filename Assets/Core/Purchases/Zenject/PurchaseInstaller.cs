using Core.Purchases.Data;
using Core.Purchases.Impl;
using Core.Tools.Repository;
using UnityEngine;
using Zenject;

namespace Core.Purchases.Zenject
{
    public class PurchaseInstaller : MonoInstaller
    {
        [SerializeField] private PurchaseRepository _repository;

        public override void InstallBindings()
        {
            Container
                .Bind<Repository<PurchaseConfig>>()
                .To<PurchaseRepository>()
                .FromInstance(_repository)
                .AsSingle();

            Container
                .BindInterfacesTo<PurchaseService>()
                .FromNew()
                .AsSingle();
        }
    }
}