namespace Delegatess
{
    public delegate void CalculateEventHandler(params int[] values);
    internal class Program
    {
        public static event CalculateEventHandler CalculateEvent;
        static void Main(string[] args)
        {
            CalculateEvent += Add;
            CalculateEvent += Add;
            CalculateEvent += Multiply;
            CalculateEvent += Subtract;
            CalculateEvent -= Add;
            CalculateEvent(4, 5, 6);
            Func<int, int, int> add = (x, y) => { return x + y; };
            Console.ReadKey();
            static void Add(int[] values)
            {
                Console.WriteLine("Addition");
                Console.WriteLine( values.Sum());
            }
            static void Multiply(int[] values)
            {
                Console.WriteLine("Multiply");
                int res = 1;
                for (int i = 0; i < values.Length; i++)
                    Console.WriteLine(res*values[i]);
            }
            static void Subtract(int[] values) {
                Console.WriteLine("Subtract");
            int res = 1;
            for(int i = 0; i < values.Length; i++) {
                    Console.WriteLine(values[i]-res);
                }
            }
            // Console.WriteLine("Hello, World!");
        }
    }
}
