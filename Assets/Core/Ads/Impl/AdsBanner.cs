using System;
using Core.Ads.Api;
using Core.Ads.Data;

namespace Core.Ads.Impl
{
    public partial class AdsService : IBannerAds
    {
        public event Action<AdMessage> OnReadyBanner;
        public event Action<AdMessage> OnFailedBanner;
        
        private void InitializeBanner()
        {
            LoadRewarded();
            IronSource.Agent.init (AdsConstants.APP_KEY, IronSourceAdUnits.BANNER);
            
            IronSourceBannerEvents.onAdLoadedEvent     += BannerOnAdLoadedEvent;
            IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        }

        private void DisposeBanner()
        {
            IronSourceBannerEvents.onAdLoadedEvent     -= BannerOnAdLoadedEvent;
            IronSourceBannerEvents.onAdLoadFailedEvent -= BannerOnAdLoadFailedEvent;
        }

        private void LoadBanner()
        {
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        }
        
        private void BannerOnAdLoadFailedEvent(IronSourceError error)
        {
            OnFailedBanner?.Invoke(new AdMessage(null, GetErrorString(error), Result.Error));
        }

        private void BannerOnAdLoadedEvent(IronSourceAdInfo info)
        {
            OnReadyBanner?.Invoke(new AdMessage(info.instanceId, null, Result.Ready));
            ShowBanner();
        }
        
        public void ShowBanner()
        {
            IronSource.Agent.displayBanner();
        }

        public void HideBanner()
        {
            IronSource.Agent.hideBanner();
            IronSource.Agent.destroyBanner();
        }
    }
}