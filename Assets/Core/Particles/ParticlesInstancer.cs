using System;
using System.Linq;
using Core.Particles.Api;
using Core.Particles.Data;
using Core.Tools.Repository;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Particles
{
    public class ParticlesInstantiater<TParticleType> : IParticlesInstantiater<TParticleType> where TParticleType : Enum
    {
        [Inject] private Repository<Particle<TParticleType>> _repository;
        
        public GameObject Instantiate(TParticleType type, Vector3 position)
        {
            var particle = _repository.First(part => part.Type.Equals(type));
            return Object.Instantiate(particle.Prefab, position, particle.Prefab.transform.rotation);
        }
    }
}
