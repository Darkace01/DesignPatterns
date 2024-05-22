namespace DesignPatterns.Solid.DependencyInversion;
// The Dependency Inversion Principle states that high-level modules should not depend on low-level modules. Both should depend on abstractions.
// The Dependency Inversion Principle is a way to decouple software modules.
// Use Abstractions where possible
public static class DependencyInversion
{
    public static void Run()
    {
        var parent = new Person("John");
        var child1 = new Person("Chris");
        var child2 = new Person("Matt");

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new Research(relationships);
    }
}
public enum Relationship
{
    Parent,
    Child,
    Sibling
}

// High-level
public class Person(string name)
{
    public string Name = name;
}

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

// Low-level
public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations = [];

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add((parent, Relationship.Parent, child));
        relations.Add((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return relations
            .Where(x => x.Item1.Name == name &&
                                   x.Item2 == Relationship.Parent)
            .Select(r => r.Item3);
    }

    //public List<(Person, Relationship, Person)> Relations => relations;
}

public class Research
{
    //public Research(Relationships relationships)
    //{
    //    var relations = relationships.Relations;
    //    foreach (var r in relations.Where(
    //                   x => x.Item1.Name == "John" &&
    //                                   x.Item2 == Relationship.Parent
    //                                          ))
    //    {
    //        WriteLine($"John has a child called {r.Item3.Name}");
    //    }
    //}
    public Research(IRelationshipBrowser browser)
    {
        foreach (var p in browser.FindAllChildrenOf("John"))
        {
            WriteLine($"John has a child called {p.Name}");
        }
    }
}