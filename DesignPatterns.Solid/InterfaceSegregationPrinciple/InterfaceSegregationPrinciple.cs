namespace DesignPatterns.Solid.InterfaceSegregationPrinciple;
// Having your interfaces be small and focused on a single purpose is a good thing.
// YAGNI - You Ain't Gonna Need It
// Split your interfaces into smaller interfaces that are focused on a single purpose. 
// Seperation of concerns comes to mind
public class InterfaceSegregationPrinciple
{
}
public class Document(string title, string content)
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
}
// Wrong way to use Interface Segregation Principle
public interface IMachine
{
    void Print(Document d);
    void Fax(Document d);
    void Scan(Document d);
}

public class MultiFunctionPrinter : IMachine
{
    public void Print(Document d)
    {
        // Print
    }

    public void Fax(Document d)
    {
        // Fax
    }

    public void Scan(Document d)
    {
        // Scan
    }
}

public class OldFashionedPrinter : IMachine
{
    public void Print(Document d)
    {
        // Print
    }

    public void Fax(Document d)
    {
        throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
        throw new NotImplementedException();
    }
}

// Better way to use Interface Segregation Principle

public interface IPrinter
{
    void Print(Document d);
}

public interface IScanner
{
    void Scan(Document d);
}

public class Printer : IPrinter
{
    public void Print(Document d)
    {
        // Print
    }
}

public class Photocopier : IPrinter, IScanner
{
    public void Print(Document d)
    {
        // Print
    }

    public void Scan(Document d)
    {
        // Scan
    }
}

public interface IMultiFunctionDevice : IPrinter, IScanner
{
}

public class MultiFunctionMachine(IPrinter printer, IScanner scanner) : IMultiFunctionDevice
{
    private IPrinter printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
    private IScanner scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));

    public void Print(Document d)
    {
        printer.Print(d);
    }

    public void Scan(Document d)
    {
        scanner.Scan(d);
    }
}