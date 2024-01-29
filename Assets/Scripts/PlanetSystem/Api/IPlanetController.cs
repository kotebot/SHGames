using PlanetSystem.Impl;
using PlanetSystem.Impl.Planets;
using UnityEngine;

namespace PlanetSystem.Api
{
    public interface IPlanetController
    {
        public GameObject Object { get; }
        
        public IPlanet Planet { get; }
        public SkinnedPlanet SkinnedPlanet { get; }
        public PlanetMover Mover { get; }
        public PlanetAttacker PlanetAttacker { get; }
        public HealthPlanet HealthPlanet { get; }

    }
}