using Core.Tools.Repository;
using UnityEngine;

namespace Core.Purchases.Data
{
    [CreateAssetMenu(fileName = "Purchases", menuName = "ScriptableObjects/PurchasesRepository", order = 1)]
    public class PurchaseRepository : Repository<PurchaseConfig>
    {
        
    }
}