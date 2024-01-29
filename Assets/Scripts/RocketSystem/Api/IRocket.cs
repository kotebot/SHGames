using PlanetSystem.Api;
using RocketSystem.Data;
using UnityEngine;

namespace RocketSystem.Api
{
    public interface IRocket : IDamageGiver
    {
        public Vector3 Position { get; }
        
        public void ChangeTrajectory(Vector3 force, Vector3 torque);

        public void Setup(RocketPreferences preferences);
        public void Push(IPlanet planet);
    }
}