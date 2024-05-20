
// Stepwise builder pattern with interface segregation principle
namespace Builder.Stepwise
{
    // Different types of car needs diferent builders
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        // Set as private to not expose the constructor of Impl outside the scope of carbuilder
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car car = new();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch(car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
                }

                car.WheelSize = size;
                return this;
            }

            public Car Build() => car;
        }

        internal static ISpecifyCarType Create() => new Impl();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
           var car = CarBuilder.Create() // ISpecifyCarType
                .OfType(CarType.Crossover) // ISpecifyWheelSize
                .WithWheels(18) // IBuildCar
                .Build();
        }
    }
}