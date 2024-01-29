using PlanetSystem.Api;
using UnityEngine;

namespace PlanetSystem.Impl.Planets
{
    
    public class PlanetController : MonoBehaviour, IPlanetController
    {
        public GameObject Object => gameObject;
        public IPlanet Planet => _planet;
        
        [field:SerializeField] public SkinnedPlanet SkinnedPlanet { get; private set; }
        [field:SerializeField] public PlanetMover Mover { get; private set; }
        [field:SerializeField] public PlanetAttacker PlanetAttacker { get; private set; }
        [field:SerializeField] public HealthPlanet HealthPlanet { get; private set; }

        [SerializeField] private Planet _planet;
    }
}