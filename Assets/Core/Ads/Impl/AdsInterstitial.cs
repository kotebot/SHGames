using System;
using Core.Ads.Api;
using Core.Ads.Data;

namespace Core.Ads.Impl
{
    public partial class AdsService : IInterstitialAds
    {
        public event Action<AdMessage> OnReadyInterstitial;
        public event Action<AdMessage> OnShowedInterstitial;
        public event Action<AdMessage> OnCloseInterstitial;
        public event Action<AdMessage> OnFailedInterstitial;
        
        private void InitializeInterstitial()
        {
            LoadInterstitial();
            IronSource.Agent.init (AdsConstants.APP_KEY, IronSourceAdUnits.INTERSTITIAL);
            
            IronSourceInterstitialEvents.onAdReadyEvent         += InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent    += InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent        += InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent       += InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent    += InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent        += InterstitialOnAdClosedEvent;
        }

        private void DisposeInterstitial()
        {
            IronSourceInterstitialEvents.onAdReadyEvent         -= InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent    -= InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent        -= InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent       -= InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent -= InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent    -= InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent        -= InterstitialOnAdClosedEvent;
        }
        
        public void ShowInterstitial(string id = AdsConstants.INTERSTITIAL_ID)
        {
            IronSource.Agent.showInterstitial(id);
        }
        
        private void LoadInterstitial()
        {
            if(IronSource.Agent.isInterstitialReady())
                IronSource.Agent.loadRewardedVideo();
        }
        
        private void InterstitialOnAdClosedEvent(IronSourceAdInfo info)
        {
            OnReadyInterstitial?.Invoke(new AdMessage(info.instanceId, null, Result.Closed));
        }

        private void InterstitialOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo info)
        {
            OnCloseInterstitial?.Invoke(new AdMessage(info.instanceId, GetErrorString(error), Result.Error));
        }

        private void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo info)
        {
            OnShowedInterstitial?.Invoke(new AdMessage(info.instanceId, null, Result.Success));
        }

        private void InterstitialOnAdClickedEvent(IronSourceAdInfo obj)
        {
        }

        private void InterstitialOnAdOpenedEvent(IronSourceAdInfo info)
        {
            OnCloseInterstitial?.Invoke(new AdMessage(info.instanceId, null, Result.Open));
        }

        private void InterstitialOnAdReadyEvent(IronSourceAdInfo info)
        {
            OnReadyInterstitial?.Invoke(new AdMessage(info.instanceId, null, Result.Ready));
        }

        private void InterstitialOnAdLoadFailed(IronSourceError error)
        {
            OnFailedInterstitial?.Invoke(new AdMessage(null, GetErrorString(error), Result.Ready));
        }
    }
}