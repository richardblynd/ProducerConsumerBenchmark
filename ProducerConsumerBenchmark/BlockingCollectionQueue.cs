using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerBenchmark
{
    public class BlockingCollectionQueue : IProducerConsumer
    {
        private readonly BlockingCollection<Action> _defaultQueue;
        private Task _dequeTask;

        public BlockingCollectionQueue()
        {
            _defaultQueue = new BlockingCollection<Action>(new ConcurrentQueue<Action>());
        }

        public void Enqueue(Action item)
        {
            if (!_defaultQueue.IsAddingCompleted)
            {
                _defaultQueue.Add(item);
            }
        }

        public void Stop()
        {
            _defaultQueue.CompleteAdding();
        }

        public void StartDequeing()
        {
            Task.Run(DequeueTask);
        }

        private void DequeueTask()
        {
            foreach (var item in _defaultQueue.GetConsumingEnumerable())
            {
                item?.Invoke();
            }
        }
    }
}
