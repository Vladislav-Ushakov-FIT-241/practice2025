using System;
using System.Threading;

namespace task14
{
    public class DefiniteIntegral
    {
        private static readonly object _syncRoot = new object();

        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
        {
            double total = 0.0;
            double segmentLength = (b - a) / threadsNumber;

            using (var barrier = new Barrier(threadsNumber + 1))
            {
                for (int i = 0; i < threadsNumber; i++)
                {
                    double start = a + i * segmentLength;
                    double end = (i == threadsNumber - 1) ? b : start + segmentLength;

                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        double partialSum = 0.0;
                        double x = start;

                        while (x < end)
                        {
                            double nextX = Math.Min(x + step, end);
                            partialSum += (function(x) + function(nextX)) * (nextX - x) / 2;
                            x = nextX;
                        }

                        lock (_syncRoot)
                        {
                            total += partialSum;
                        }

                        barrier.SignalAndWait();
                    });
                }
                barrier.SignalAndWait();
            }
            return total;
        }
    }
}
