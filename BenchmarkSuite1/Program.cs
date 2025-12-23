using BenchmarkDotNet.Running;
using ML_Math_Image_Process;

namespace BenchmarkSuite1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _ = BenchmarkRunner.Run<PixelManipulationBenchmark>();
        }
    }
}
