using System.Text;

namespace Factory.BulkReplacement
{
    public interface ITheme
    {
        string TextColor { get; }
        string BgrColor { get; }
    }

    class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BgrColor => "white";
    }

    class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BgrColor => "dark gray";
    }

    // Additional benefits of factory, it can keep track of every object it creates
    // MUST USE WeakReference when keeping track of objects so it does not extend the lifetime of them ( otherwise objects will live as long as the factory lives )
    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in themes)
                {
                    if (reference.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> themes = new();

        public ITheme CreateThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var Reference = new Ref<ITheme>(CreateThemeImpl(dark));
            themes.Add(new(Reference));
            return Reference;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach(var wr in themes)
            {
                if(wr.TryGetTarget(out var reference))
                {
                    reference.Value = CreateThemeImpl(dark);
                }
            }
        }

    }

    // Reference Wrapper only usefull to bulk replacement
    public class Ref<T> where T : class
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(false);
            var theme2 = factory.CreateTheme(true);
            Console.WriteLine(factory.Info);
            // Dark theme
            // Light theme


            // replacement
            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.Value.BgrColor); // dark gray
            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.Value.BgrColor); // white
        }
    }
}
