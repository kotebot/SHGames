using RocketSystem.Api;
using UnityEngine;

namespace PlanetSystem.Api
{
    public interface IPlanet : IGravityChanger
    {
        Vector3 Position { get; }

        public void Setup(float mass, float gravityRadius, float diameter);
    }
}
