using System.Collections.Generic;

namespace PlanetSystem.Api
{
    public interface IPlanetsList
    {
        public IReadOnlyList<IPlanetController> Planets { get; }

        public void DestroyPlanet(IPlanetController planetController);
    }
}