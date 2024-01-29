using System;
using System.Collections.Generic;
using PlanetSystem.Impl.Planets;
using RocketSystem.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace PlanetSystem.Data
{
    [CreateAssetMenu(fileName = "PlanetsConfiguration", menuName = "ScriptableObjects/Planets/PlanetsConfiguration", order = 1)]
    public class PlanetsConfiguration : SerializedScriptableObject
    {
        /// <summary>
        /// Include player planet. Range between 2, 8. 
        /// </summary>
        [BoxGroup("Planets Info")] public int AmountPlanets;
        
        [BoxGroup("Planets Info")] public int MinimumDistanceBetweenPlanet;
        [BoxGroup("Planets Info")] public int MaximumDistanceBetweenPlanet;
        [BoxGroup("Planets Info")] public List<PlanetPreferences> PlanetPreferences;
        
        [FoldoutGroup("Prefabs"), AssetsOnly] public PlanetController PlanetPrefab;

    }

    [Serializable]
    public class PlanetPreferences
    {
        [BoxGroup("Planet Info")] public bool IsPlayer;

        [BoxGroup("Physics Info")] public float GravityRadius;
        [BoxGroup("Physics Info")] public float Mass;
        [BoxGroup("Physics Info")] public float Diameter;
        [BoxGroup("Physics Info")] public float Speed;
        
        [BoxGroup("Attack Info")] public List<AmountRocketsOnPlanet> AmountRockets;
        [BoxGroup("Attack Info")] public float HealthPoint;

    }

    [Serializable]
    public struct AmountRocketsOnPlanet
    {
        public RocketType RocketType;
        public int Amount;
    }
    
 
}