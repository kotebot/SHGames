using System;

namespace Core.Purchases.Data
{
    [Serializable]
    public class PurchaseConfig : Tools.Repository.Data
    {
        public string IOSId;
        public float AmountReward;
    }
}