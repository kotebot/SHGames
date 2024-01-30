using System;
using Core.Ads.Api;
using Core.Tools.UnityEvents;
using SafeKepper;
using Zenject;

namespace Core.Ads.Impl
{
    public partial class AdsService : IAdsService
    {
        [Inject] private IUnityEventsHandler _unityEventsHandler;

        public event Action OnDisabledAds;
        
        public bool DisabledAds { private set; get; }

        private const string DisableAdsKey = "DisableAds";
        
        public void Initialize()
        {
            IronSource.Agent.init(AdsConstants.APP_KEY);
             
            _unityEventsHandler.OnApplicationPauseEvent += OnApplicationPauseEvent;
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

            DisabledAds = Saver.GetBool(DisableAdsKey, false);

            LoadAds();
        }
        
        public void Dispose()
        {
            _unityEventsHandler.OnApplicationPauseEvent -= OnApplicationPauseEvent;
            IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;

            DisposeRewarded();
            DisposeBanner();
            DisposeInterstitial();
        }
        
        public void DisableAds()
        {
            DisabledAds = true;
            OnDisabledAds?.Invoke();
            Saver.SetBool(DisableAdsKey, true);
        }

        private void LoadAds()
        {
            InitializeRewarded();
            InitializeBanner();
            InitializeInterstitial();
        }

        private void SdkInitializationCompletedEvent()
        {
            IronSource.Agent.validateIntegration();
        }

        private void OnApplicationPauseEvent(bool isPause)
        {
            IronSource.Agent.onApplicationPause(isPause);
        }
        
        private string GetErrorString(IronSourceError error)
        {
            return $"{error.getErrorCode()}\n{error.getDescription()}";
        }
    }
}
