using System;
using System.Threading.Tasks;
using UnityEngine.Purchasing;
using Zenject;

namespace Core.Purchases.Api
{
    public interface IPurchaseService: IInitializable
    {
        public event Action OnInitializated;
        public event Action<Product> OnPurchased;
        public bool Initialized { get; }
        
        public void Purchase(string id);
        public void Purchase(Product product);

        Task InitializeAsync();
    }
}