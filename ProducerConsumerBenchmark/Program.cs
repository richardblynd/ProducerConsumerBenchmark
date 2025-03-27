using BenchmarkDotNet.Running;

namespace ProducerConsumerBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MultipleJobBenchMark>();
        }
    }
}
