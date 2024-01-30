using System;
using Core.Ads.Data;

namespace Core.Ads.Api
{
    public interface IInterstitialAds 
    {
        public event Action<AdMessage> OnReadyInterstitial;
        public event Action<AdMessage> OnShowedInterstitial;
        public event Action<AdMessage> OnCloseInterstitial;
        public event Action<AdMessage> OnFailedInterstitial;

        public void ShowInterstitial(string id);
    }
}