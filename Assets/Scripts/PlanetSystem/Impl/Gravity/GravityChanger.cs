using System.Collections.Generic;
using System.Linq;
using RocketSystem.Api;
using UnityEngine;

namespace PlanetSystem.Impl.Gravity
{
    public class GravityChanger
    {
        private readonly LayerMask _rocketMask = LayerMask.GetMask("Rocket"); 
        
        private readonly Vector3 _position;
        private readonly float _gravityRadius;
        private readonly float _mass;
        
        public GravityChanger(Vector3 position, float gravityRadius, float mass)
        {
            _position = position;
            _gravityRadius = gravityRadius;
            _mass = mass;
        }
        
        public IEnumerable<IRocket> FoundNearRockets()
        {
            var rockets = 
                Physics.OverlapSphere(_position, _gravityRadius, _rocketMask)
                .Select(rocket => rocket.GetComponent<IRocket>());

            return rockets;
        }

        public void AttractRockets(IEnumerable<IRocket> rockets)
        {
            foreach (var rocket in rockets)
            {
                var forcePosition = GetForcePosition(rocket.Position);
                
                rocket.ChangeTrajectory(forcePosition , forcePosition);
            }
        }

        private Vector3 GetForcePosition(Vector3 positionRocket)
        {
            return ((_position - positionRocket).normalized * _mass) * (_gravityRadius / (_position - positionRocket).magnitude);
        }

        public void DrawGizmos(Color color = default)
        {
            if (color == default)
                Gizmos.color = Color.red;
            else
                Gizmos.color = color;
            
            Gizmos.DrawWireSphere(_position, _gravityRadius);
        }
    }
}