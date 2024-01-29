using UnityEngine;

namespace PlanetSystem.Api
{
    public interface ISun : IGravityChanger
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}