using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalsTimer
{
    /// <summary>
    /// Defines a class which implements a timer.
    /// </summary>
    public class KalTimer : ITimer
    {
        /// <summary>
        /// Gets the time that timer started at.
        /// </summary>
        public DateTime StartDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date that the timer has ended at.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total amount of time that has elapsed while the timer has run.
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                return EndDate.GetValueOrDefault(DateTime.Now) - StartDate;
            }
        }

        /// <summary>
        /// Gets the total number of miliseconds that have elapsed while the timer has run.
        /// </summary>
        public double TotalMiliseconds
        {
            get
            {
                return TotalTime.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = null;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            this.EndDate = DateTime.Now;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
