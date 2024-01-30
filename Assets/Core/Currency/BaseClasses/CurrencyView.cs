using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Currency.BaseClasses
{
    public abstract class CurrencyView<TType> : MonoBehaviour where TType : Enum
    {
        private ACurrency<TType> _currency;
        private IWallet<TType> _wallet;
        
        [SerializeField] private TMP_Text _text;
        
        protected abstract TType Type { get; }

        [Inject]
        private void Construct(IWallet<TType> wallet)
        {
            _wallet = wallet;
        }

        private void Start()
        {
            _currency = _wallet.GetCurrency(Type);
            
            _currency.OnAmountChange += CurrencyOnAmountChange;
            CurrencyOnAmountChange(_currency.Amount);
        }

        private void OnDestroy()
        {
            _currency.OnAmountChange -= CurrencyOnAmountChange;
        }

        private void CurrencyOnAmountChange(float amount)
        {
            _text.SetText(amount.ToString(CultureInfo.InvariantCulture));
        }
    }
}