using Newtonsoft.Json;

namespace Builder.Faceted
{
    public class Person
    {
        // address
        public string? StreetAddress, PostCode, City;

        // employment
        public string? CompanyName, Position;
        public int? AnnualIncome;

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    // Facade
    public class PersonBuilder
    {
        // reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb) => pb.person;
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string company)
        {
            person.CompanyName = company;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postCode)
        {
            person.PostCode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 asdf road")
                      .In("asdf")
                      .WithPostCode("ad221LMT")
                .Works.At("Fabrica")
                      .AsA("Engineer")
                      .Earning(1230000);
            Console.WriteLine(person);
        }
    }
}
