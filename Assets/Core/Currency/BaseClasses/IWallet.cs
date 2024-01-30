using System;

namespace Core.Currency.BaseClasses
{
    public interface IWallet<TCurrencyType>  where TCurrencyType : Enum
    {
        public ACurrency<TCurrencyType> GetCurrency(TCurrencyType type);
        public void AddCurrency(TCurrencyType type);
        public void RemoveCurrency(TCurrencyType type);
        
        public event Action<ACurrency<TCurrencyType>, float> OnAnyCurrencyChanged;
    }
}