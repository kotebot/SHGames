using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Core.Purchases.Api
{
    public interface IProductRepository
    {
        public Product NoAdsProduct { get; }
        public IReadOnlyList<Product> Products { get; }
    }
}