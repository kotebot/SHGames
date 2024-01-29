using System.Collections.Generic;
using System.Linq;
using Core.Tools;
using Core.Tools.Repository;
using PlanetSystem.Api;
using PlanetSystem.Data;
using UnityEngine;
using Zenject;
using Core.Zenject;
using PlanetSystem.Impl.Attackers;
using RocketSystem.Data;
using Random = UnityEngine.Random;

namespace PlanetSystem.Impl.Planets
{
    public class PlanetSpawner : IPlanetSpawner, IPlanetsList
    {
        [Inject] private Repository<PlanetSkin> _planetSkins;
        [Inject] private Repository<RocketPreferences> _rocketPreferences;
        [Inject] private ISun _sun;
        [Inject] private DiContainer _diContainer;
        [Inject] private IPlayerAttacker _playerAttacker;

        public IReadOnlyList<IPlanetController> Planets => _planets;

        private List<IPlanetController> _planets = new List<IPlanetController>();
        private Transform _parentObject;

        public void SpawnPlanets(PlanetsConfiguration configuration)
        {
            if(!IsValidConfiguration(configuration))
                return;

            var shuffledSkins = _planetSkins.ToList();
            shuffledSkins.Shuffle();

            _parentObject = new GameObject("PlanetsContainer").GetComponent<Transform>();//create container for planets
            
            for (int i = 0; i < configuration.AmountPlanets; i++)
            {
                var previousPosition = i == 0 ? _sun.Position : _planets[i - 1].Planet.Position;

                var spawnedPlanet = SpawnPlanet(configuration.PlanetPrefab,
                    shuffledSkins[i], 
                    configuration.PlanetPreferences[i],
                    previousPosition,
                    Random.Range(configuration.MinimumDistanceBetweenPlanet,
                        configuration.MaximumDistanceBetweenPlanet)
                    );
                
                _planets.Add(spawnedPlanet);
            }
        }
        
        
        public void DestroyPlanet(IPlanetController planetController)
        {
            _planets.Remove(planetController);
            
            Object.Destroy(planetController.Object);
        }

        private IPlanetController SpawnPlanet(PlanetController planetPrefab, PlanetSkin skin, PlanetPreferences preferences, Vector3 previousObject, float range)
        {
            var position = new Vector3(previousObject.x + range, previousObject.y, previousObject.z);

            var spawnedPlanet = _diContainer.Instantiate(planetPrefab, _parentObject, position);
            
            SetupPlanet(skin, preferences, spawnedPlanet);

            return spawnedPlanet.GetComponent<IPlanetController>();
        }

        private void SetupPlanet(PlanetSkin skin, PlanetPreferences preferences, PlanetController spawnedPlanet)
        {
            spawnedPlanet.SkinnedPlanet.Setup(skin);
            spawnedPlanet.Planet.Setup(preferences.Mass, preferences.GravityRadius, preferences.Diameter);
            spawnedPlanet.Mover.Setup(preferences.Speed);
            spawnedPlanet.HealthPlanet.Setup(preferences.HealthPoint);

            if (preferences.IsPlayer)
                _playerAttacker.Setup(preferences.AmountRockets);

            IInputAttacker attacker = preferences.IsPlayer
                ? _playerAttacker
                : new AIAttacker(preferences.AmountRockets, _rocketPreferences);

            spawnedPlanet.PlanetAttacker.Setup(attacker);
        }
        
        /// <returns>Return false if not valid config, GD must be check this.</returns>
        private bool IsValidConfiguration(PlanetsConfiguration configuration)
        {
            if (configuration.AmountPlanets is < 2 or > 8 || configuration.AmountPlanets != configuration.PlanetPreferences.Count())
            {
                Debug.LogError("Not correct AmountPlanets in configuration");
                return false;
            }
            
            if (configuration.MinimumDistanceBetweenPlanet > configuration.MaximumDistanceBetweenPlanet)
            {
                Debug.LogError("Not correct MinimumDistanceBetweenPlanet or MaximumDistanceBetweenPlanet in configuration");
                return false;
            }
            
            if (configuration.PlanetPrefab == null)
            {
                Debug.LogError("Not correct setted PlanetPrefab in configuration");
                return false;
            }
            
            if (configuration.PlanetPreferences.Count(pl => pl.IsPlayer) != 1)
            {
                Debug.LogError("Player more to one, or not have player in configuration");
                return false;
            }
            
            return true;
        }

    }
}