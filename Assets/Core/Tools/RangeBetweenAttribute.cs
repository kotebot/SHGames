using System;

namespace Core.Tools
{
    public class RangeBetweenAttribute: Attribute
    {
        private int Minimum;
        private int Maximum;
        
        public RangeBetweenAttribute(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }
    }
}