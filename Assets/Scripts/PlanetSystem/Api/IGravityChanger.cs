using System.Collections.Generic;
using PlanetSystem.Impl.Gravity;
using RocketSystem.Api;

namespace PlanetSystem.Api
{
    public interface IGravityChanger
    {
        public GravityChanger GravityChanger { get; }
        
        public float GravityRadius { get; }
        public float Mass { get; }

        public IEnumerable<IRocket> FoundNearRockets()
        {
            return GravityChanger.FoundNearRockets();
        }

        public void AttractRockets(IEnumerable<IRocket> rockets)
        {
            GravityChanger.AttractRockets(rockets);
        }
    }
}