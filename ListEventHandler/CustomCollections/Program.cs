using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace CustomCollections
{
    public delegate void ListChangedEventHandler<T>(string action, T? item);
    public delegate void ListChangedEventHandler1<T>(string action, T? item);
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new MyList<Dictionary<string,int>>();
            //ListChangedEventHandler l = (action, item) => Console.WriteLine($"[ADD2] {action}: {item} ");
            ListChangedEventHandler <int>l = delegate (string s, int k)
            {
                Console.WriteLine(3);
            };
            //ListChangedEventHandler l1 = l;
            l += (action, item) => Console.WriteLine($"[ADD1] {action}: {item} ");
            //l1.Invoke("Hello", default);
            l.Invoke("Hi", default);


            ////l += (action, item) => throw new Exception("Exception");
            ////l +=(action, item) => Console.WriteLine($"[ADD2] {action}: {item} ");
            ////l.Invoke("Hello",default);

            //var list = new MyList<String>();
            //int a = 0;
            //list.ItemAdded += (action, item) => Console.WriteLine($"[ADD1] {action}: {item} : {a}");
            //a++;
            //a++;
            //list.ItemAdded += (action, item) => Console.WriteLine($"[ADD2] {action}: {item} {a}");
            //int addCount = 0;
            ////list.ItemAdded += (action, item) =>
            ////{
            ////    int b = 1;
            ////    Console.WriteLine($"[ADD3] {addCount++}: {item}");
            ////};
            //addCount = 0;
            //list.ItemRemoved += (action, item) => Console.WriteLine($"[REMOVE] {addCount++}:{action}: {item}");
            ////list.ItemRemoved += (action, item) => throw new Exception("Exception");
            ////list.ItemRemoved += (action, item) => Console.WriteLine($"[REMOVE] {addCount}:{action}: {item}");
            //list.ListCleared += (action, item) => Console.WriteLine($"[CLEAR] {action}");
            //list.Add("Prod1");
            //list.Add("Prod2");
            //list.Add("Prod3");
            //list.Remove("Prod1");

            //list.Remove("Prod2");
            //list.Clear();
            //Console.WriteLine("Add count: " + addCount);
            ///*foreach (var l in list)
            //{
            //    Console.WriteLine($"Remaining : {l}");
            //}
            //*/
            //list.Clear();
            //Console.WriteLine($"Count after clearing the list:{list.Count}");
            Console.ReadKey();
        }
        //}
        public class MyList<T> : List<T> 
        {
            public event ListChangedEventHandler<T>? ItemAdded;
            public event ListChangedEventHandler<T>? ItemRemoved;
            public event ListChangedEventHandler<T>? ListCleared;
            public new void Add(T item)
            {
                base.Add(item);
                ItemAdded?.Invoke("Added", item);
            }
            public new bool Remove(T item)
            {
                bool removed = base.Remove(item);
                if (removed)
                {
                    ItemRemoved?.Invoke("Removed", item);
                }
                return removed;
            }
            public new void Clear()
            {
                base.Clear();
                ListCleared?.Invoke("Cleared", default);
            }
        }
    }
}
