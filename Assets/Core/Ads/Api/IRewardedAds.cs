using System;
using Core.Ads.Data;

namespace Core.Ads.Api
{
    public interface IRewardedAds
    {
        public event Action<AdMessage> OnReadyRewarded;
        public event Action<AdMessage> OnShowedRewarded;
        public event Action<AdMessage> OnCloseRewarded;
        public event Action<AdMessage> OnFailedRewarded;

        public void ShowRewarded(string id);
    }
}