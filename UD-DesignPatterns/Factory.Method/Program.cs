using Newtonsoft.Json;

namespace Factory.Method
{
    public class Point
    {
        // factory methods
        public static Point NewCartesianPoint(double x, double y) => new(x, y);

        public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));

        public double x { get; private set; }
        public double y { get; private set; }

        private Point(double x, double y)
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
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
