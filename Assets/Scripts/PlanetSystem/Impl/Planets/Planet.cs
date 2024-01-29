using PlanetSystem.Api;
using PlanetSystem.Impl.Gravity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlanetSystem.Impl.Planets
{
    public class Planet: MonoBehaviour, IPlanet
    {
        public GravityChanger GravityChanger { get; private set; }
        
        [field:SerializeField, BoxGroup("Setting")] public float GravityRadius { get; private set; }
        [field:SerializeField, BoxGroup("Setting"), HideIf(nameof(AutoMass))] public float Mass { get; private set; }
#if UNITY_EDITOR
        /// <summary>
        /// Used only for showing in editor
        /// </summary>
        [field:SerializeField, BoxGroup("Setting"), ShowIf(nameof(AutoMass)), ReadOnly] public float DefaultMass { get; private set; }

#endif
        
        [BoxGroup("Setting")] public bool AutoMass = false;
        
        [SerializeField, FoldoutGroup("Editor Setting")] private Color GizmosColor;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            if (AutoMass)
                Mass = transform.localScale.x;
            
            GravityChanger = new GravityChanger(transform.position, GravityRadius, Mass);
        }

        private void FixedUpdate()
        {
            GravityChanger.AttractRockets(GravityChanger.FoundNearRockets());;
        }
        
        public void Setup(float mass, float gravityRadius, float diameter)
        {
            Mass = mass;
            GravityRadius = gravityRadius;
            transform.localScale = new Vector3(diameter, diameter,diameter);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            GravityChanger = new GravityChanger(transform.position, GravityRadius, Mass);
            DefaultMass = transform.localScale.x;
        }

        private void OnDrawGizmosSelected()
        {
            GravityChanger.DrawGizmos(GizmosColor);
        }
#endif
    }
}