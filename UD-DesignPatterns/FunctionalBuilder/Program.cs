namespace FunctionalBuilder
{
    public class Person
    {
        public string? Name, Position;
    }
    /* Non Generic version of FunctionalBuilder
    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions = new();

        public PersonBuilder Called(string name) => Do(p => p.Name = name);

        private PersonBuilder AddAction(Action<Person> action)
        {
            // Added action must return the Person itself to be fluent
            actions.Add(p => { action(p); return p; });

            // AddAction can be fluent or not, set as fluent to be well rounded
            return this;
        }

        // Exposes the AddAction Method
        public PersonBuilder Do(Action<Person> action) => AddAction(action);

        public PersonBuilder Build() => actions.Aggregate(new Person(), (p, f) => f(p));
    }
    */

    public abstract class FunctionalBuilder<TSubject, TSelf> 
        where TSelf : FunctionalBuilder<TSubject,TSelf>
        where TSubject : new()
    {
        // List of functions that takes an Person an returns an person
        private readonly List<Func<Person, Person>> actions = new();

        private TSelf AddAction(Action<Person> action)
        {
            // Added action must return the Person itself to be fluent
            actions.Add(p => { action(p); return p; });

            // AddAction can be fluent or not, set as fluent to be well rounded
            return (TSelf)this;
        }

        // Exposes the AddAction Method
        public TSelf Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

    }

    public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name) => Do(p => p.Name = name);
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Jao")
                .WorksAs("Developer")
                .Build();
        }
    }
}