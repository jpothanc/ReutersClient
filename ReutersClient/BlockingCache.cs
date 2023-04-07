using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReutersClient
{
    internal class BlockingCache<T>
    {
        private readonly Func<T, Task>? _handler;
        private BlockingCollection<T> _collection;
        public BlockingCache(int bucketSize, Func<T, Task> handler)
        {
            _collection = new BlockingCollection<T>(bucketSize);
            _handler = handler;
            Task.Run(Process);
        }

        public void Add(T data)
        {
            _collection.Add(data);
        }
        private async Task Process()
        {
            while (!_collection.IsCompleted)
            {
                try
                {
                    var data = _collection.Take();
                    _handler?.Invoke(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
