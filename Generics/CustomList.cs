using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    internal class CustomList<T> 
        where T: class,IDisposable
    {
        private List<T> list = new List<T>();
        public bool Add(T item)
        {   
            list.Add(item);
            return true;
        }   
    }
    public class ListExtensions
    {
        public int Count<T>(List<T> list)
        {
            return list.Count;
        }
        public int Add<T>(List<T> list, T item) 
        {
            return 1;
        }
}
