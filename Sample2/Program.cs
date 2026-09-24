using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Assignments
{
    /// <summary>
    /// program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// division
        /// </summary>
        /// <param name="a">dcs</param>
        /// <param name="b">vfsc</param>
        /// <returns>fdcefv</returns>
        public static int Dividewith(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// rvf
        /// </summary>
        /// <param name="a">fedc</param>
        /// <param name="b">fvs</param>
        /// <returns>fdcsrf</returns>
        public static int Dividewithout(int a, int b)
        {
            if (b != 0)
            {
                return a / b;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// main
        /// </summary>
        /// <param name="args">args</param>
        private static void Main(string[] args)
        {
            int a = 100;
            Stopwatch watch=new Stopwatch();
            for (int i = 0; i < 1000000; i++)
            {
                Dividewith(a, i);
            }

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();

            for (int i = 0 ; i < 1000000; i++)
            { 
                Dividewithout(a, i);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}