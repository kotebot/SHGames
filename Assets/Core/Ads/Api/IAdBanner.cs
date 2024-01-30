using System;

namespace Core.Ads.Api
{
    public interface IAdBanner
    {
        public event Action OnLoadBanner;
        public event Action OnClickToBanner;
        
        public void ShowBanner(string id);
        public void HideBanner(string id);
    }
}