using System;

namespace MVCConventions.Attributes
{
    public class MinLengthAttribute : Attribute
    {
        public int MinLength { get; private set; }

        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }
    }
}