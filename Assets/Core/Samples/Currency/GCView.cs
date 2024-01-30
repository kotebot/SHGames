using Core.Currency.BaseClasses;

namespace Core.Samples.Currency
{
    public class GCView : CurrencyView<MyCurrencyType>
    {
        protected override MyCurrencyType Type => MyCurrencyType.GC;
    }
}