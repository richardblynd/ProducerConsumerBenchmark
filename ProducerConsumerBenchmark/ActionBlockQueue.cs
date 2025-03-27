using System.Threading.Tasks.Dataflow;

namespace ProducerConsumerBenchmark
{
    public class ActionBlockQueue : IProducerConsumer
    {
        private readonly ActionBlock<Action> _actionBlock;
        private Task _dequeTask;

        public ActionBlockQueue()
        {
            var dataflowOptions = new ExecutionDataflowBlockOptions() { EnsureOrdered = true, MaxDegreeOfParallelism = 1 };
            _actionBlock = new ActionBlock<Action>(item => item?.Invoke(), dataflowOptions);
        }

        public void Enqueue(Action item)
        {
            _actionBlock.Post(item);
        }

        public void StartDequeing()
        {
        }

        public void Stop()
        {
            _actionBlock.Complete();
        }
    }
}
