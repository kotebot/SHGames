using PlanetSystem.Data;

namespace PlanetSystem.Api
{
    public interface IPlanetSpawner
    {
        public void SpawnPlanets(PlanetsConfiguration configuration);
    }
}