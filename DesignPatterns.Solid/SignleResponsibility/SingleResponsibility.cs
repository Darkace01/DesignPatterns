namespace DesignPatterns.Solid.SignleResponsibility;
public static class SingleResponsibility
{
    public static void Run()
    {
        var j = new Journal();
        j.AddEntry("I cried today.");
        j.AddEntry("I ate a bug.");
        WriteLine(j);

        var filename = @"c:\temp\journal.txt";
        Persistence.SaveToFile(j, filename, true);
    }
}
public class Journal
{
    private readonly List<string> entries = [];
    private static int count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count; // memento
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
}

// Separation of concerns, this handles the persistence of the journal
public static class Persistence
{
    public static void SaveToFile(Journal journal, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
        {
            File.WriteAllText(filename, journal.ToString());
        }
    }
}