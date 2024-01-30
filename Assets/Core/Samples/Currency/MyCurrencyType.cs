using Core.Tools;

namespace Core.Samples.Currency
{
    public enum MyCurrencyType
    {
        [LinkedType(typeof(GachaCurrency))]
        GC
    }
}