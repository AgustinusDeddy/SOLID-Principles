namespace OpenClosePrinciple.Specification
{
    public class ColorSpecification : ISpecification<Product>
    {
        private Enums.Color color;

        public ColorSpecification(Enums.Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product p)
        {
            return p.Color == color;
        }
    }
}