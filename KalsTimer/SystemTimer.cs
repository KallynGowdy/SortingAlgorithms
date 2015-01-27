using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalsTimer
{
    /// <summary>
    /// Defines a wrapper for the system timer.
    /// </summary>
    public class SystemTimer : ITimer
    {
        private Stopwatch watch = new Stopwatch();

        public double TotalMiliseconds
        {
            get { return (double)watch.ElapsedMilliseconds; }
        }

        public void Start()
        {
            watch.Start();
        }

        public void Stop()
        {
            watch.Start();
        }

        public void Reset()
        {
            watch.Reset();
        }
    }
}
