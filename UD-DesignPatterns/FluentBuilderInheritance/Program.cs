// Fluent builder inheritance with recursive generics
namespace FluentBuilderInheritance
{
    public class Person
    {
        public string Name;
        public string Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public class PersonInfoBuilder
    {
        protected Person person = new Person();

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

    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new PersonJobBuilder();
            builder.Called("caio")
                .WorksAsA // problem
        }
    }
}