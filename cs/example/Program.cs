namespace Example
{
    public class Program
    {
        public static void Main() 
        {
            Foo foo = new Foo(); 
            IncrementHelper.Increment(foo); 
            System.Console.WriteLine("foo: "+ foo.Value); 

            Bar bar = new Bar(); 
            IncrementHelper.Increment(bar); 
            System.Console.WriteLine("bar: "+ bar.Value); 
        }
    }

    class Foo
    {
        public int Value { get; set; }
    }

    struct Bar
    {
        public int Value { get; set; }
    }

    static class IncrementHelper
    {
        public static void Increment(Foo foo)
        {
            foo.Value += 1;
        }
        
        public static void Increment(Bar bar)
        {
            bar.Value += 1;
        }
    } 

}
