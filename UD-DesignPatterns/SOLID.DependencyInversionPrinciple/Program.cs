using static System.Console;

namespace SOLID.DependencyInversionPrinciple
{
    /// <summary>
    /// High level modules should not depend on low level modules; both should depend on abstractions
    /// </summary>

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string? Name;
        // public DateTime DateOfBirth;
        // ETC..
    }

    #region Bad Implementation
    /*
    // Low-level
    public class Relationships
    {
        private List<(Person, Relationship, Person)> relations
            = new();

        public void AddParentAndChild(Person parent, Person Child)
        {
            relations.Add((parent, Relationship.Parent, Child));
            relations.Add((Child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => relations;
    }

    // High-Level
    public class Research
    {
        // Access low-level property, the class Relationships cannot change how to store the data
        public Research(Relationships relationships)
        {
            var relations = relationships.Relations;
            foreach (var r in relations.Where(
                x => x.Item1.Name == "John" &&
                x.Item2 == Relationship.Parent))
            {
                WriteLine($"John has a child called {r.Item3.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();

            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
    */
    #endregion

    #region Principle Applied
    // Low-level
    // Now Relationships can change how the relations are stored.
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations
            = new();

        public void AddParentAndChild(Person parent, Person Child)
        {
            relations.Add((parent, Relationship.Parent, Child));
            relations.Add((Child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name) =>
            relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
            .Select(r => r.Item3);
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // High-Level
    public class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach(var p in browser.FindAllChildrenOf("John"))
                WriteLine($"John has a child called {p.Name}");
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();

            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
    #endregion

}
