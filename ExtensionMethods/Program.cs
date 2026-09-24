namespace ExtensionMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String mail = "charu@gmail.com";
            if (mail.IsEmail())   Console.WriteLine("true");
        }
    }
}
