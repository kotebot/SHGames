using System;
using Core.Ads.Api;
using Core.Ads.Data;

namespace Core.Ads.Impl
{
    public partial class AdsService : IRewardedAds
    {
        public event Action<AdMessage> OnReadyRewarded;
        public event Action<AdMessage> OnShowedRewarded;
        public event Action<AdMessage> OnCloseRewarded;
        public event Action<AdMessage> OnFailedRewarded;

        private void InitializeRewarded()
        {
            LoadRewarded();
            IronSource.Agent.init (AdsConstants.APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
            
            IronSourceRewardedVideoEvents.onAdOpenedEvent      += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent      += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent   += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent  += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent    += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent     += RewardedVideoOnAdClickedEvent;
        }

      
        private void DisposeRewarded()
        {
            IronSourceRewardedVideoEvents.onAdOpenedEvent      -= RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent      -= RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent   -= RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent  -= RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent    -= RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent     -= RewardedVideoOnAdClickedEvent;
        }

        public void ShowRewarded(string id = AdsConstants.REWARDED_ID)
        {
            if(IronSource.Agent.isRewardedVideoAvailable())
                IronSource.Agent.showInterstitial(id);
        }
        
        private void LoadRewarded()
        {
            IronSource.Agent.loadRewardedVideo();
        }

        private void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo info)
        {
        }

        private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo info)
        {
            OnShowedRewarded?.Invoke(new AdMessage(info.instanceId, null, Result.Success));
        }

        private void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo info)
        {
            OnCloseRewarded?.Invoke(new AdMessage(info.instanceId, GetErrorString(error), Result.Error));
        }

        private void RewardedVideoOnAdUnavailable()
        {
        }

        private void RewardedVideoOnAdAvailable(IronSourceAdInfo info)
        {
            OnReadyRewarded?.Invoke(new AdMessage(info.instanceId, null, Result.Ready));
        }

        private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo info)
        {
            OnCloseRewarded?.Invoke(new AdMessage(info.instanceId, null, Result.Closed));
        }

        private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo info)
        {
            OnCloseInterstitial?.Invoke(new AdMessage(info.instanceId, null, Result.Open));
        }

    }
}