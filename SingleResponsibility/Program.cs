using System;
using System.Collections.Generic;
using OpenClosePrinciple;

namespace SingleResponsibility
{

    public class ProductFilter
    {
        // let's suppose we don't want ad-hoc queries on products
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Enums.Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Enums.Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Enums.Size size, Enums.Color color)
        {
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        } // state space explosion
        // 3 criteria = 7 methods

        // OCP = open for extension but closed for modification
    }

    // we introduce two new interfaces that are open for extension

    public interface ISpecification<T>
    {
        bool IsSatisfied(Product p);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

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

    // combinator
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null)
                throw new ArgumentNullException(paramName: nameof(first));

            if (second == null)
                throw new ArgumentNullException(paramName: nameof(second));

            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(Product p)
        {
            return first.IsSatisfied(p) && second.IsSatisfied(p);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Enums.Color.Green, Enums.Size.Small);
            var tree = new Product("Tree", Enums.Color.Green, Enums.Size.Large);
            var house = new Product("House", Enums.Color.Blue, Enums.Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");

            foreach (var p in pf.FilterByColor(products, Enums.Color.Green))
                Console.WriteLine($" - {p.Name} is green");

            // ^^ BEFORE

            // vv AFTER
            var bf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (var p in bf.Filter(products, new ColorSpecification(Enums.Color.Green)))
                Console.WriteLine($" - {p.Name} is green");

            Console.WriteLine("Large products");
            foreach (var p in bf.Filter(products, new SizeSpecification(Enums.Size.Large)))
                Console.WriteLine($" - {p.Name} is large");

            Console.WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(new ColorSpecification(Enums.Color.Blue), new SizeSpecification(Enums.Size.Large)))
            )
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
