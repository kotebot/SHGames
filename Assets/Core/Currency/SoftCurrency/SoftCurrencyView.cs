using Core.Currency.BaseClasses;
using Core.Currency.Data;

namespace Core.Currency.SoftCurrency
{
    public class SoftCurrencyView : CurrencyView<CurrencyType>
    {
        protected override CurrencyType Type => CurrencyType.Soft;
    }
}