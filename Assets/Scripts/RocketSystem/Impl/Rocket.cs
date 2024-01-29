using System;
using PlanetSystem.Api;
using RocketSystem.Api;
using RocketSystem.Data;
using UnityEngine;

namespace RocketSystem.Impl
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket: MonoBehaviour, IRocket
    {
        public Vector3 Position => transform.position;
        public float Damage { get; private set; }

        private Rigidbody _rigidbody;
        private RocketPreferences _preferences;
        
        private void Awake()
        {
            InitRigidbody();
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        public void ChangeTrajectory(Vector3 force, Vector3 torque)
        {
            _rigidbody.AddForce(force, ForceMode.Force);
            _rigidbody.AddTorque(torque, ForceMode.Force);
        }

        public void Setup(RocketPreferences preferences)
        {
            InitRigidbody();
            _preferences = preferences;
            
            _rigidbody.mass = preferences.Weight;
            Damage = preferences.Damage;

            Instantiate(preferences.Model, transform);
        }

        public void Push(IPlanet planet)
        {
            _rigidbody.AddForce((planet.Position - transform.position).normalized * _preferences.Acceleration, ForceMode.Impulse);
        }

        private void InitRigidbody()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }
    }
}