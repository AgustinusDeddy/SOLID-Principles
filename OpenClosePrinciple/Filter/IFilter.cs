using System.Collections.Generic;
using OpenClosePrinciple.Specification;

namespace OpenClosePrinciple.Filter
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
}