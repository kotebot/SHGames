using System;
using System.Collections.Generic;
using Core.Currency.BaseClasses;
using Core.Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Currency
{
    public class Wallet<TCurrencyType> : IWallet<TCurrencyType>, IInitializable where TCurrencyType : Enum
    {
        private readonly Dictionary<TCurrencyType, ACurrency<TCurrencyType>> _currencies = new ();

        public event Action<ACurrency<TCurrencyType>, float> OnAnyCurrencyChanged;

        public virtual void Initialize()
        {
            foreach (TCurrencyType type in Enum.GetValues(typeof(TCurrencyType)))
            {
                AddCurrency(type);
            }
        }
        
        public void AddCurrency(TCurrencyType type)
        {
            if(_currencies.ContainsKey(type))
                throw new ArgumentException($"{type} is already added");

            _currencies.Add(type, InitCurrency(type));
        }
        
        public void RemoveCurrency(TCurrencyType type)
        {
            if(!_currencies.ContainsKey(type))
                throw new ArgumentException($"{type} is already added");
            
            _currencies.Remove(type);
        }

        public ACurrency<TCurrencyType> GetCurrency(TCurrencyType type)
        {
            if (_currencies.TryGetValue(type, out var currency))
                return currency;
            
            throw new ArgumentException($"{type} is not available");
        }

        private ACurrency<TCurrencyType> InitCurrency(TCurrencyType type)
        {
            var currencyType = type.GetLinkedType();
            ACurrency<TCurrencyType> currency = null;
            try
            {
                currency = (ACurrency<TCurrencyType>)Activator.CreateInstance(currencyType);

            }
            catch (Exception e)
            {
                Debug.LogError($"Can`t create instance for {type}.\nCheck linked Type in Enum. Stack trace: \n{e}");
                throw;
            }

            currency.OnAmountChange += amount => OnAnyCurrencyChanged?.Invoke(currency, amount);
            return currency;
        }
    }
}