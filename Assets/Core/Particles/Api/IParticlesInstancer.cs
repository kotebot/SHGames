using System;
using UnityEngine;

namespace Core.Particles.Api
{
    public interface IParticlesInstantiater<TParticleType> where TParticleType : Enum
    {
        public GameObject Instantiate(TParticleType type, Vector3 position);
    }
}