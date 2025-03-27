namespace ProducerConsumerBenchmark
{
    public interface IProducerConsumer
    {
        void Enqueue(Action item);
        void Stop();
        void StartDequeing();
    }
}
