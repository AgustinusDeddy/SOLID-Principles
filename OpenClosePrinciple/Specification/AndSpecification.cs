using System;

namespace OpenClosePrinciple.Specification
{
    // combinator
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first != null)
                this.first = first;
             else
                throw new ArgumentNullException(paramName: nameof(first));

            if (second != null)
                this.second = second;
            else
                throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(Product p)
        {
            return first.IsSatisfied(p) && second.IsSatisfied(p);
        }
    }
}