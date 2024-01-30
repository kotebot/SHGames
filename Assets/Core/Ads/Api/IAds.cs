using System;
using Core.Ads.Data;

namespace Core.Ads.Api
{
    public interface IAds
    {
        public event Action<AdMessage> OnReady;
        public event Action<AdMessage> OnShowed;
        public event Action<AdMessage> OnClose;
        public event Action<AdMessage> OnFailed;

        public void Show(string id);
    }
}