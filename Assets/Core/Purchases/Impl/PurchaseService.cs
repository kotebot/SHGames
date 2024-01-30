using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Ads.Api;
using Core.Purchases.Api;
using Core.Purchases.Data;
using Core.Tools;
using Core.Tools.Repository;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Core.Purchases.Impl
{
    public partial class PurchaseService : IPurchaseService, IDetailedStoreListener, IProductRepository
    {
        [Inject] private Repository<PurchaseConfig> _repository;
        [Inject] private IAdsService _ads;

        public event Action OnInitializated;
        public event Action<Product> OnPurchased;

        public bool Initialized { get; private set; }
        private string NoAdsOfferId => Extensions.GetIdWithPlatform(PurchasesConstants.NO_ADS_ANDROID_ID, PurchasesConstants.NO_ADS_IOS_ID);

        public Product NoAdsProduct => _storeController?.products.WithID(NoAdsOfferId);
        public IReadOnlyList<Product> Products => _storeController?.products.all;

        public void Initialize()
        {
        }

        public async Task InitializeAsync()
        {
            try {

#if UNITY_EDITOR
                await Task.Delay(1);
#else
                await UnityServices.InitializeAsync();
                Debug.Log("Unity services inited");

#endif
                
                var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
                foreach (var config in _repository)
                {
                    builder.AddProduct(config.Id, ProductType.Consumable, new IDs
                    {
                        {config.Id, GooglePlay.Name},
                        {config.IOSId, MacAppStore.Name}
                    });
                }

                builder.AddProduct(PurchasesConstants.NO_ADS_ANDROID_ID, ProductType.NonConsumable, new IDs
                {
                    {PurchasesConstants.NO_ADS_ANDROID_ID, GooglePlay.Name},
                    {PurchasesConstants.NO_ADS_IOS_ID, MacAppStore.Name}
                });
                Debug.Log("Products created");

            
                UnityPurchasing.Initialize(this, builder);
                Debug.Log("Start UnityPurchasing init");
                Initialized = true;
                OnInitializated?.Invoke();
            }
            catch (Exception exception) {
                Debug.LogError(exception); 
            }
        }


        public void Purchase(string id)
        {
            _storeController.InitiatePurchase(id);
        }
        
        public void Purchase(Product product)
        {
            _storeController.InitiatePurchase(product);
        }

        private void ReceiveReward(Product product)
        {
            if (product.definition.id == NoAdsOfferId)
            {
                _ads.DisableAds();
            }
            OnPurchased?.Invoke(product);
        }
    }
}