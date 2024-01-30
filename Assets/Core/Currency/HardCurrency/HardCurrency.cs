using Core.Currency.BaseClasses;
using Core.Currency.Data;

namespace Core.Currency.HardCurrency
{
    public class BaseHardCurrency : ACurrency<CurrencyType>
    {
        public override CurrencyType Type => CurrencyType.Soft;
        public override float BaseValue => 1000;
    }
}