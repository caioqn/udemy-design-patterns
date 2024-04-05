// Fluent builder inheritance with recursive generics
namespace FluentBuilderInheritance
{
    public class Person
    {
        public string? Name;

        public string? Position;

        #region allows the fluent interface to work
        public class Builder : PersonJobBuilder<Builder> { }

        public static Builder New => new();
        #endregion

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }


    #region fluent builder with inheritance problem
    /*
    public class PersonInfoBuilder
    {
        protected Person person = new();

        public PersonInfoBuilder Called(string name)
        {
            person.Name = name;
            return this;
        }
    }

    public class PersonJobBuilder : PersonInfoBuilder
    {
        public PersonJobBuilder WorksAsA(string position)
        {
            person.Position = position;
            return this;
        }
    }
    */
    #endregion

    #region fluent builder with design to solve the problem and use inheritance
    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    // class Foo : Bar<Foo> 
    public class PersonInfoBuilder<SELF>
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            /* Problematic Builder
            var pb = new PersonJobBuilder();
            var badme = pb
                .Called("caio")
                .WorksAsA("asdf"); // doesnt Work
            */

            // Working Builder
            var me = Person.New
                .Called("Caio")
                .WorksAsA("asdf")
                .Build();
            Console.WriteLine(me);
        }
    }
}