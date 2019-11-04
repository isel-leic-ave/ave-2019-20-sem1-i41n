using BenchmarkDotNet.Running;

namespace LoggerBench
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LoggerBench>();
        }
    }
}
