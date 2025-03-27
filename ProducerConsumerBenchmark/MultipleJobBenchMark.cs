using BenchmarkDotNet.Attributes;

namespace ProducerConsumerBenchmark
{
    public class MultipleJobBenchMark
    {
        private AutoResetEvent _autoResetEvent;

        public MultipleJobBenchMark()
        {
            _autoResetEvent = new AutoResetEvent(false);
        }

        [Benchmark]
        public void BlockingCollectionQueue()
        {
            DoMultipleJobs(new BlockingCollectionQueue());
        }

        [Benchmark]
        public void DataFlowQueue()
        {
            DoMultipleJobs(new DataFlowQueue());
        }

        [Benchmark]
        public void ActionBlockQueue()
        {
            DoMultipleJobs(new ActionBlockQueue());
        }

        [Benchmark]
        public void ChannelQueue()
        {
            DoMultipleJobs(new ChannelQueue());
        }

        private void DoMultipleJobs(IProducerConsumer producerConsumerQueue)
        {
            producerConsumerQueue.StartDequeing();
            int jobs = 100000;

            for (int i = 0; i < jobs - 1; i++)
            {
                producerConsumerQueue.Enqueue(() => { });
            }

            producerConsumerQueue.Enqueue(() => _autoResetEvent.Set());
            _autoResetEvent.WaitOne();
            producerConsumerQueue.Stop();
        }
    }
}
