using System;
using UnityEngine;

namespace RocketSystem.Data
{
    [Serializable]
    public class RocketPreferences : Core.Tools.Repository.Data
    {
        public new int Id => (int)Type;
        
        public RocketType Type;

        public GameObject Model;
        
        public float Damage;
        public float Acceleration;
        public float Weight;
        public float Cooldown;
    }
}