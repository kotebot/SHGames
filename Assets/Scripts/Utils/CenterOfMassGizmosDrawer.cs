using System;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Rigidbody))]
    public class CenterOfMassGizmosDrawer : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, Range(0.01f, 1f)] private float _radius = 0.1f;
        
        private Rigidbody _rigidbody;
        
        private void OnValidate()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDrawGizmos()
        {
            if (_rigidbody == null)
                throw new NullReferenceException(nameof(_rigidbody) + " is null");
            
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawSphere(transform.position + _rigidbody.centerOfMass, _radius);
        }
#endif
    }
}
