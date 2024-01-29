using System.Collections.Generic;
using PlanetSystem.Data;

namespace PlanetSystem.Api
{
    public interface IPlayerAttacker : IInputAttacker
    {
        public void Setup(List<AmountRocketsOnPlanet> amountPlanets);
    }
}