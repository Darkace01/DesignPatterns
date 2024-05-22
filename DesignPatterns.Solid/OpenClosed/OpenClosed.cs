namespace DesignPatterns.Solid.OpenClosed;
public static class OpenClosed
{
    public static void Run()
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };
        // Wrong way to use Open-Closed Principle
        // BEFORE

        Console.WriteLine("Green products (old):");
        foreach (var p in ProductFilter.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($" - {p.Name} is green");
        }

        // Better way to use Open-Closed Principle
        // AFTER
        var bf = new BetterFilter();
        Console.WriteLine("Green products (new):");
        foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        {
            Console.WriteLine($" - {p.Name} is green");
        }

        Console.WriteLine("Large blue items:");
        foreach (var p in bf.Filter(
               products,
                  new AndSpecification<Product>(
                             new ColorSpecification(Color.Blue),
                                    new SizeSpecification(Size.Large)
                                       )))
        {
            Console.WriteLine($" - {p.Name} is big and blue");
        }
    }
}
public enum Color
{
    Red, Green, Blue
}

public enum Size
{
    Small, Medium, Large, Yuge
}

public class Product(string name, Color color, Size size)
{
    public string Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
    public Color Color = color;
    public Size Size = size;
}

public class ProductFilter
{
    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (var p in products)
            if (p.Size == size)
                yield return p;
    }

    public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (var p in products)
            if (p.Color == color)
                yield return p;
    }

    public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
        foreach (var p in products)
            if (p.Color == color && p.Size == size)
                yield return p;
    }
}

public interface ISpecification<T>
{
    bool IsSatisfied(T t);
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification(Color color) : ISpecification<Product>
{
    private readonly Color color = color;

    public bool IsSatisfied(Product p)
    {
        return p.Color == color;
    }
}

public class SizeSpecification(Size size) : ISpecification<Product>
{
    private readonly Size size = size;

    public bool IsSatisfied(Product p)
    {
        return p.Size == size;
    }
}

public class AndSpecification<T>(ISpecification<T> first, ISpecification<T> second) : ISpecification<T>
{
    private readonly ISpecification<T> first = first ?? throw new ArgumentNullException(paramName: nameof(first)), second = second ?? throw new ArgumentNullException(paramName: nameof(second));

    public bool IsSatisfied(T t)
    {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
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