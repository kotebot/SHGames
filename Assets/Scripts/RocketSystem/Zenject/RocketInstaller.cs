using Core.Tools.Repository;
using RocketSystem.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RocketSystem.Zenject
{
    public class RocketInstaller : MonoInstaller
    {
        [SerializeField][AssetsOnly] private RocketsRepository _rocketRepository;

        public override void InstallBindings()
        {
            Container
                .Bind<Repository<RocketPreferences>>()
                .To<RocketsRepository>()
                .FromInstance(_rocketRepository)
                .AsSingle();

        }
    }
}