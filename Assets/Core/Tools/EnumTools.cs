using System;

namespace Core.Tools
{
    public static class EnumTools
    {
        public static TEnumElement[] GetElements<TEnumElement>() where TEnumElement : struct
        {
            return (TEnumElement[])Enum.GetValues(typeof(TEnumElement));
        }
    }
}