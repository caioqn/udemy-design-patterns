using Newtonsoft.Json;

namespace Factory.Method
{
    public static class PointFactory
    {
        // factory methods inside a factory class
        public static Point NewCartesianPoint(double x, double y) => new(x, y);

        public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }

    public class Point
    {
        /* factory methods inside the class
        public static Point NewCartesianPoint(double x, double y) => new(x, y);

        public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
        */

        public double x { get; private set; }
        public double y { get; private set; }

        // To use an factory class, the constructor needs to be public
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
