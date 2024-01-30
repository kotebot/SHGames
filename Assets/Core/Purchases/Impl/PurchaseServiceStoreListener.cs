using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Core.Purchases.Impl
{
    public partial class PurchaseService
    {
        private IStoreController _storeController;
        private IExtensionProvider _extensions;

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError(error.ToString());
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError(error + "\n" + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (purchaseEvent.purchasedProduct.hasReceipt)
            {
                ReceiveReward(purchaseEvent.purchasedProduct);
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError(product.definition.id + "\n" + failureReason.ToString());
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensions = extensions;
            Debug.Log("End UnityPurchasing init");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError(failureDescription.productId + "\n" + failureDescription.reason + "\n" +
                           failureDescription.message);
        }
    }
}