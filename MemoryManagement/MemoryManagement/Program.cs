namespace MemoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MemoryEater me = new MemoryEater();
            me.Allocate();
        }
    }
}
