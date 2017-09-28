namespace OpenClosePrinciple.Specification
{
    public class SizeSpecification : ISpecification<Product>
    {
        private Enums.Size size;

        public SizeSpecification(Enums.Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product p)
        {
            return p.Size == size;
        }
    }
}