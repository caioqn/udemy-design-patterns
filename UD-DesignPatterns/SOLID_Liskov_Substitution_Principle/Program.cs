using static System.Console;

namespace SOLID_Liskov_Substitution_Principle
{
    /// <summary>
    /// Objects of a superclass shall be replaceable with objects of its subclasses without breaking the application,
    /// In this case Rectangle sq = new Square(), as an upcast using inheritance
    /// </summary>

    #region Bad Implementation

    /*
    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle() { }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string? ToString() => 
            $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }

    public class Square : Rectangle
    {
        public new int Width
        {
            set { base.Width = base.Height = value; }
        }

        public new int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    */
    #endregion

    #region Principle applied
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle() { }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string? ToString() =>
            $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    #endregion

    internal class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
