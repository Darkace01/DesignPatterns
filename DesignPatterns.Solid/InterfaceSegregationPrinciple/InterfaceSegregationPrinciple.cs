namespace DesignPatterns.Solid.InterfaceSegregationPrinciple;
// Having your interfaces be small and focused on a single purpose is a good thing.
public class InterfaceSegregationPrinciple
{
}
public class Document
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Document(string title, string content)
    {
        Title = title;
        Content = content;
    }
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

public class MultiFunctionMachine : IMultiFunctionDevice
{
    private IPrinter printer;
    private IScanner scanner;

    public MultiFunctionMachine(IPrinter printer, IScanner scanner)
    {
        this.printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
        this.scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
    }

    public void Print(Document d)
    {
        printer.Print(d);
    }

    public void Scan(Document d)
    {
        scanner.Scan(d);
    }
}