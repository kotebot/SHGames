using System;
using Core.Particles.Data;
using Core.Tools.Repository;
using Particles;
using Zenject;

namespace Core.Particles.Zenject
{
    public class ParticleInstaller
    {
        public static void InstallBindings<TParticleType>(DiContainer container, ParticleRepository<TParticleType> repository) where TParticleType : Enum
        {
            container
                .Bind<Repository<Particle<TParticleType>>>()
                .FromInstance(repository)
                .AsSingle(); 

            container
                .BindInterfacesTo<ParticlesInstantiater<TParticleType>>()
                .FromNew()
                .AsSingle(); 
        }
    }
}