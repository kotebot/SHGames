using Core.Purchases.Data;
using Core.Tools.Repository;
using Core.Zenject;
using PlanetSystem.Api;
using PlanetSystem.Data;
using PlanetSystem.Impl;
using PlanetSystem.Impl.Attackers;
using PlanetSystem.Impl.Planets;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PlanetSystem.Zenject
{
    public class PlanetInstaller : MonoInstaller
    {
        [SerializeField, SceneObjectsOnly] private Sun _sun;
        [SerializeField, SceneObjectsOnly] private PlayerAttacker _playerAttacker;
        
        [SerializeField, AssetsOnly] private PlanetSkinsRepository _planetSkinsRepository;
        [SerializeField, AssetsOnly] private PlanetsConfiguration _planetsConfiguration;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ISun>()
                .FromInstance(_sun)
                .AsSingle();
            
            Container
                .Bind<Repository<PlanetSkin>>()
                .To<PlanetSkinsRepository>()
                .FromInstance(_planetSkinsRepository)
                .AsSingle();

            Container
                .Bind<PlanetsConfiguration>()
                .FromInstance(_planetsConfiguration)
                .AsSingle();

            Container
                .Bind<IPlayerAttacker>()
                .FromInstance(_playerAttacker)
                .AsSingle();
            
            Container
                .Bind<IPlanetSpawner, IPlanetsList>()
                .To<PlanetSpawner>()
                .FromNew()
                .AsSingle();
            
            Container.Resolve<IPlanetSpawner>().SpawnPlanets(Container.Resolve<PlanetsConfiguration>());//move to level controller
        }
    }
}