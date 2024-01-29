using Core.Tools.Repository;
using UnityEngine;

namespace RocketSystem.Data
{
    [CreateAssetMenu(fileName = "RocketsRepository", menuName = "ScriptableObjects/Rockets/RocketsRepository", order = 1)]
    public class RocketsRepository : Repository<RocketPreferences>
    {
        
    }
}