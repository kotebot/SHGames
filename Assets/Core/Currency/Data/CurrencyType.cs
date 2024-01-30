using Core.Currency.HardCurrency;
using Core.Currency.SoftCurrency;
using Core.Tools;

namespace Core.Currency.Data
{
    public enum CurrencyType
    {
        [LinkedType(typeof(BaseSoftCurrency))]
        Soft,
        [LinkedType(typeof(BaseHardCurrency))]
        Hard,
    }
}