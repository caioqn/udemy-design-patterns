namespace Factory.AsynchronousMethod
{
    public class Foo
    {
        private Foo() { }

        private async Task<Foo> InitAsyn()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsyn();
        }
    }


    internal class Program
    {
        static async void Main(string[] args)
        {
            Foo x = await Foo.CreateAsync();
        }
    }
}
