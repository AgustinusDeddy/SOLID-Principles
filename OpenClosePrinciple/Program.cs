using System;
using OpenClosePrinciple.Filter;
using OpenClosePrinciple.Specification;


namespace OpenClosePrinciple
{
    public class Program
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
                Console.WriteLine ($" - {p.Name} is green");

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

            Console.ReadLine();

        }
    }
}
