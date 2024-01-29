using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Tools.Repository;
using PlanetSystem.Api;
using PlanetSystem.Data;
using RocketSystem.Data;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlanetSystem.Impl.Attackers
{
    public class AIAttacker : IInputAttacker
    {
        public event Action<RocketType> OnPushRocket;

        private List<AmountRocketsOnPlanet> _amountPlanets;
        private Repository<RocketPreferences> _repository;
        private IDisposable _coroutineDisposable;
        
        public AIAttacker(List<AmountRocketsOnPlanet> amountPlanets, Repository<RocketPreferences> repository)
        {
            _amountPlanets = new List<AmountRocketsOnPlanet>(amountPlanets);
            _repository = repository;
            
            _coroutineDisposable = Observable.FromCoroutine(CooldownRoutine)
                .Subscribe();
        }
        
        
        public void Stop()
        {
            _coroutineDisposable.Dispose();
        }

        private IEnumerator CooldownRoutine()
        {
            while (_amountPlanets.Count > 0)
            {
                var cooldown = PushRocket(_amountPlanets[Random.Range(0, _amountPlanets.Count)]);
                yield return new WaitForSeconds(cooldown);
            }
        }

        /// <returns>Cooldown for next shoot</returns>
        private float PushRocket(AmountRocketsOnPlanet amountRockets)
        {
            var indexOfPlanet= _amountPlanets.IndexOf(amountRockets);
            amountRockets.Amount--;

            if (amountRockets.Amount <= 0)
            {
                _amountPlanets.Remove(amountRockets);
            }
            else
            {
                _amountPlanets[indexOfPlanet] = amountRockets;
            }
            
            OnPushRocket?.Invoke(amountRockets.RocketType);

            return _repository.First(preference => preference.Type == amountRockets.RocketType).Cooldown;
        }
    }
}