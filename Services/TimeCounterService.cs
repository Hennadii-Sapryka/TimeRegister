using System;
using System.Diagnostics;

namespace TimeRegisterApp.Services
{
    public class TimeCounterService
    {
        private static Stopwatch stopWatch;

        public TimeCounterService()
        {
            if (stopWatch == null)
                stopWatch = new Stopwatch();
        }

        public string TimeCounter(string button)
        {
            if (button == "Start" | !stopWatch.IsRunning)
            {
                stopWatch.Start();
                TimeSpan ts = stopWatch.Elapsed;
                return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            }
            else
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                stopWatch.Reset();
                return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }
    }
}