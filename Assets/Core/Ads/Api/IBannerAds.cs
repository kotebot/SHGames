using System;
using Core.Ads.Data;

namespace Core.Ads.Api
{
    public interface IBannerAds
    {
        public event Action<AdMessage> OnReadyBanner;
        public event Action<AdMessage> OnFailedBanner;

        public void ShowBanner();
        public void HideBanner();
        
    }
}