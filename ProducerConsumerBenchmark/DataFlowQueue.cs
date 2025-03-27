using System.Threading.Tasks.Dataflow;

namespace ProducerConsumerBenchmark
{
    public class DataFlowQueue : IProducerConsumer
    {
        private readonly BufferBlock<Action> _bufferBlock;
        private Task _dequeTask;

        public DataFlowQueue()
        {
            var dataflowOptions = new DataflowBlockOptions() { EnsureOrdered = true };
            _bufferBlock = new BufferBlock<Action>(dataflowOptions);
        }

        public void Enqueue(Action item)
        {
            _bufferBlock.Post(item);
        }

        public void StartDequeing()
        {
            _dequeTask = Task.Run(DequeueTask);
        }

        private async Task DequeueTask()
        {
            while (await _bufferBlock.OutputAvailableAsync())
            {
                while (_bufferBlock.TryReceive(out var item))
                {
                    item?.Invoke();
                }
            }
        }

        public void Stop()
        {
            _bufferBlock.Complete();
        }
    }
}