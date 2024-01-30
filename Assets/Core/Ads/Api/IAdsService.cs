using System;
using Zenject;

namespace Core.Ads.Api
{
    public interface IAdsService : IInitializable, IDisposable
    {
        public event Action OnDisabledAds;
        public bool DisabledAds { get; }

        public void DisableAds();
    }
}