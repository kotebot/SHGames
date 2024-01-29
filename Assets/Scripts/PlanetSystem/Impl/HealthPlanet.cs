using System;
using PlanetSystem.Api;
using PlanetSystem.Impl.Planets;
using RocketSystem.Api;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlanetSystem.Impl
{
    [RequireComponent(typeof(PlanetController))]
    public class HealthPlanet : MonoBehaviour, IHealthable
    {
        [Inject] private IPlanetsList _planetsList;

        [SerializeField] private Image _hpBar;
        
        public float Hp { get; private set; }
        private float _maxHp;
        
        public void Setup(float health)
        {
            Hp = _maxHp = health;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IDamageGiver>(out var damageGiver))
            {
                TakeDamage(damageGiver.Damage);
            }
        }

        public void TakeDamage(float damage)
        {
            Hp -= damage;
            
            _hpBar.fillAmount = Hp/_maxHp;
            
            if(Hp <= 0)
                _planetsList.DestroyPlanet(GetComponent<PlanetController>());
        }
    }
}