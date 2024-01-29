using PlanetSystem.Data;
using UnityEngine;

namespace PlanetSystem.Impl.Planets
{
    public class SkinnedPlanet : MonoBehaviour
    {
        public void Setup(PlanetSkin skin)
        {
            Instantiate(skin.Skin, transform);
        }
    }
}