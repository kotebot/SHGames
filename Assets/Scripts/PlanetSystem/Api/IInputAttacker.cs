using System;
using RocketSystem.Data;

namespace PlanetSystem.Api
{
    public interface IInputAttacker
    {
        public event Action<RocketType> OnPushRocket;

        public void Stop();
    }
}