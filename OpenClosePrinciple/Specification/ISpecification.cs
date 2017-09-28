namespace OpenClosePrinciple.Specification
{
    // we introduce two new interfaces that are open for extension
    public interface ISpecification<T>
    {
        bool IsSatisfied(Product p);
    }
}