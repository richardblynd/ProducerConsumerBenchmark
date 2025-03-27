using System.Threading.Channels;

namespace ProducerConsumerBenchmark
{
    public class ChannelQueue : IProducerConsumer
    {
        private readonly Channel<Action> _channel;
        private readonly ChannelWriter<Action> _channelWriter;
        private readonly ChannelReader<Action> _channelReader;

        public ChannelQueue()
        {
            _channel = Channel.CreateUnbounded<Action>(new UnboundedChannelOptions() { SingleReader = true });
            _channelWriter = _channel.Writer;
            _channelReader = _channel.Reader;
        }

        public void Enqueue(Action item)
        {
            _channelWriter.TryWrite(item);
        }

        public void StartDequeing()
        {
            Task.Run(DequeueTask);
        }

        private async Task DequeueTask()
        {
            while (await _channelReader.WaitToReadAsync())
            {
                while (_channelReader.TryRead(out var job))
                {
                    job?.Invoke();
                }
            }
        }

        public void Stop()
        {
            _channelWriter.Complete();
        }
    }
}
