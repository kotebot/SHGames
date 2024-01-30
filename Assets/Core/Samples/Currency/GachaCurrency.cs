using Core.Currency.BaseClasses;

namespace Core.Samples.Currency
{
    public class GachaCurrency : ACurrency<MyCurrencyType>
    {
        public override MyCurrencyType Type => MyCurrencyType.GC;
        public override float BaseValue => 322;
    }
}