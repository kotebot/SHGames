using System;
using UnityEngine;

namespace Core.Particles.Data
{
    [Serializable]
    public class Particle<TParticleType> : Tools.Repository.Data  where TParticleType : Enum
    {
        public GameObject Prefab;
        public TParticleType Type;
    }
}