using MonteCarlo;
using System.Diagnostics;

static void Run(long n, int threads = 1)
{
    IMonteCarlo monteCarlo;
    if (threads == 1)
    {
        monteCarlo = new MonteCarloSerial();
    } else
    {
        monteCarlo = new MonteCarloParallel(threads);
    }
    
    var sw = Stopwatch.StartNew();
    double pi = monteCarlo.Calc(n);
    sw.Stop();
    
    Console.WriteLine($"{threads}, {n}, {sw.ElapsedMilliseconds}, {pi}");
}


Console.WriteLine("Threads, n, SpentTime (ms), pi");

long n = (long)Math.Pow(2, 28);
//Run(n, 1);
//Run(n, 2);
Run(n, 4);
//Run(n, 8);
//Run(n, 16);
//Run(n, 32);



