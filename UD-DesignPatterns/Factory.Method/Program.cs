using Newtonsoft.Json;

namespace Factory.Method
{
    public class Point
    {
        /* factory methods inside the class
        public static Point NewCartesianPoint(double x, double y) => new(x, y);

        public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
        */

        public double x { get; private set; }
        public double y { get; private set; }

        // Factory Property ( Creates a new instance every time it's called )
        public static Point OriginProp => new Point(0, 0);
        // Factory Field ( A field that is initialized once )
        public static Point OriginSingleton = new Point(0, 0); // Overall better

        // To use an factory class, the constructor needs to be public or internal ( Only accessible within the same assembly )
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        // the other way around is to use an inner factory ( declaring it inside the class being instantiated )
        /* Using static property
        public static PointFactory Factory => new();

        public class PointFactory
        {
            // factory methods inside a factory class
            public Point NewCartesianPoint(double x, double y) => new(x, y);

            public Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
        */

        // Using static class and methods
        public static class Factory
        {
            // factory methods inside a factory class
            public static Point NewCartesianPoint(double x, double y) => new(x, y);

            public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }

    }
}
