using System;
using PlanetSystem.Api;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PlanetSystem.Impl.Planets
{
    public class PlanetMover : MonoBehaviour
    {
        [Inject] private ISun _sun;

        [SerializeField, BoxGroup("Settings")] private float _speed = 10f;
        [SerializeField, BoxGroup("Settings")] private Vector3 _axis = new Vector3(0, 0, 1);

        private void FixedUpdate()
        {
            transform.RotateAround(_sun.Position, _axis, _speed * Time.fixedDeltaTime);
        }
        
        public void Setup(float speed)
        {
            _speed = speed;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(!Application.isPlaying)
                return;
            
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(new Vector3(0, transform.position.y, 0), Quaternion.identity, new Vector3(1, 0.01f, 1));
            Gizmos.DrawWireSphere(_sun.Position, Vector3.Distance(_sun.Position, transform.position));
            Gizmos.matrix = oldMatrix;
        }
#endif
    }
}