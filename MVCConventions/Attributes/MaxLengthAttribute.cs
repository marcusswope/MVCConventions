using System;

namespace MVCConventions.Attributes
{
    public class MaxLengthAttribute : Attribute
    {
        public int MaxLength { get; private set; }

        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}