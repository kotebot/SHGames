using System;
using Core.Tools.Repository;

namespace Core.Particles.Data
{
    public abstract class ParticleRepository<TParticleType> : Repository<Particle<TParticleType>> where TParticleType : Enum
    {
        
    }
}