using Core.Currency.BaseClasses;
using Core.Currency.Data;
using Core.Samples.Currency;
using Core.Tools.UnityEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ExampleUnityEvents : MonoBehaviour
{
    [Inject] private IUnityEventsHandler _unityEventsHandler;
    [Inject] private IWallet<CurrencyType> _wallet;

    private void Start()
    {
        _unityEventsHandler.OnApplicationPauseEvent += UnityEventsHandlerOnOnApplicationPauseEvent;
    }

    [Button]
    private void LogWallet()
    {
        Debug.Log(_wallet.GetCurrency(CurrencyType.Soft).Amount);
    }

    private void UnityEventsHandlerOnOnApplicationPauseEvent(bool obj)
    {
        Debug.Log("Pause");
    }
}
