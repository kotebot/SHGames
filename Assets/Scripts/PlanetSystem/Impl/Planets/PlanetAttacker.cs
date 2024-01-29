using System;
using System.Linq;
using Core.Tools.Repository;
using Core.Zenject;
using PlanetSystem.Api;
using RocketSystem.Data;
using RocketSystem.Impl;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PlanetSystem.Impl.Planets
{
    public class PlanetAttacker : MonoBehaviour, IPlanetAttacker
    {
        [Inject] private Repository<RocketPreferences> _rocketRepository;
        [Inject] private DiContainer _container;
        [Inject] private IPlanetsList _planetsList;
        
        [SerializeField, AssetsOnly] private Rocket _rocket;
        [SerializeField, ChildGameObjectsOnly] private Transform _rocketSpawnPoint;
        
        private IInputAttacker _inputAttacker;
        
        public void Setup(IInputAttacker inputAttacker)
        {
            _inputAttacker = inputAttacker;
            
            _inputAttacker.OnPushRocket += OnPushRocket;
        }

        private void OnDestroy()
        {
            _inputAttacker.Stop();
        }

        private void OnPushRocket(RocketType rocketType)
        {
            var rocketPreferences = _rocketRepository.FirstOrDefault(preferences => preferences.Type == rocketType);

            var spawnedRocket = _container.Instantiate(_rocket, null, _rocketSpawnPoint.position);
            
            spawnedRocket.Setup(rocketPreferences);
            
            var nearPlanet = _planetsList.Planets
                .OrderBy(planet => Vector3.Distance(planet.Planet.Position, transform.position))
                .First()
                .Planet;
            
            spawnedRocket.Push(nearPlanet);
        }
    }
}