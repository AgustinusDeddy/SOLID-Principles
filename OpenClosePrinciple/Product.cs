using System;

namespace OpenClosePrinciple
{
    public class Product
    {
        public string Name;
        public Enums.Color Color;
        public Enums.Size Size;

        public Product(string name, Enums.Color color, Enums.Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Color = color;
            Size = size;
        }
    }
}
