namespace MakeDi
{
    class Program
    {
        static void Main(string[] args)
        {
            var make = new Make();
            var factory = make.Get<IFactory>();
            var product = factory.Create("SomeProduct");
            var root = make.Get<IServiceA>();
        }
    }
}
