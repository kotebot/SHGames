using System;
using UnityEngine;

namespace PlanetSystem.Impl
{
    public class RotatorToCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(_cameraTransform);
        }
    }
}