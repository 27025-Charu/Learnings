
namespace MemoryManagement
{
    internal class MemoryEater
    {
        List<int[]> memAlloc = new List<int[]>();
        long initialMemory = GC.CollectionCount(0);
        internal void Allocate()
        {
            long totalMemory = GC.GetTotalMemory(false);
            while (true)
            {
                int currentGenZero = GC.CollectionCount(0);
                memAlloc.Add(new int[1000000]);
                Console.WriteLine($@"--------------------------------------------
Allocated {memAlloc.Count} million integers.
Current heap size: {totalMemory / 1024.0:F2} KB
Total Gen 0 Allocations: {currentGenZero-initialMemory}
--------------------------------------------");

                Thread.Sleep(1000); // Slow down the allocation to observe memory usage
            }
        }
    }
}