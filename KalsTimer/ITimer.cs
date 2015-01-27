using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalsTimer
{
    /// <summary>
    /// Defines an interface which represents a timer/stopwatch.
    /// </summary>
    public interface ITimer
    {

        double TotalMiliseconds { get; }

        void Start();

        void Stop();

        void Reset();

    }
}
