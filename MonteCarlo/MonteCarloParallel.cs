using Microsoft.ConcurrencyVisualizer.Instrumentation;
using System.Threading.Tasks;

namespace MonteCarlo
{
    class MonteCarloParallel(int numThreads) : IMonteCarlo
    {
        public double Calc(long n)
        {
            long pointsPerThreads = n / numThreads;
            long[] result = new long[numThreads];


            // todo: play games with marker series
            Profiler.ProfilerSpan mappingSpan = Profiler.EnterSpan("Mapping");
            Profiler.WriteFlag(2, 3, "Starting Mapping");
            Parallel.For(0, numThreads, i =>
            {
                Thread.CurrentThread.Name = "Parallel Monte Carlo #" + i;
                Profiler.WriteMessage("START_Thread_" + i);
                long insideCircle = MonteCarloHelper.CalcPoints(pointsPerThreads);
                result[i] += insideCircle;
                Profiler.WriteMessage("END_Thread_" + i);
            });
            Profiler.Leave(mappingSpan);

            Profiler.ProfilerSpan reduceSpan = Profiler.EnterSpan("Starting Reduce");
            Profiler.WriteFlag("Starting Reducing");
            long reduced = 0;
            for (int i = 0; i < result.Length; i++)
            {
                reduced += result[i];
            }
            Profiler.WriteFlag("End Reduce");
            Profiler.Leave(reduceSpan);

            return MonteCarloHelper.CalcPi(reduced, n);
        }
    }
}
