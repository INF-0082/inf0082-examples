#if DEBUG
#define ENABLE_CONCURRENCY_VISUALIZER
#endif
using Microsoft.ConcurrencyVisualizer.Instrumentation;

namespace MonteCarlo
{
    public static class Profiler
    {

        public class ProfilerSpan(Span span)
        {
            public Span Span { get; set; } = span;
        }

        public static ProfilerSpan EnterSpan(string spanName)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            return new ProfilerSpan(Markers.EnterSpan(spanName));
#else
            return new ProfilerSpan(null);
#endif
        }

        public static void Leave(ProfilerSpan span)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            span.Span.Leave();
#endif
        }

        public static void WriteMessage(string message)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            Markers.WriteMessage(message);
#endif
        }

        public static void WriteFlag(string message)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            Markers.WriteFlag(message);
#endif
        }

        public static void WriteFlag(int level, int category, string message)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            Markers.WriteFlag((Importance) level, category, message);
#endif
        }


        public static void WriteAlert(string alert)
        {
#if ENABLE_CONCURRENCY_VISUALIZER
            Markers.WriteAlert(alert);
#endif
        }
    }
}
