using System.Security.Cryptography.X509Certificates;

namespace Generics
{
    internal class Sample : IDisposable
    {
        public void Dispose() { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomList<Sample> cList = new CustomList<Sample>();
            ListExtensions lext= new ListExtensions();
            //cList.Add(new Sample());
            List<int> intList = [];
            lext.Add(intList,5);
            lext.Count(intList);
        }
    }
}