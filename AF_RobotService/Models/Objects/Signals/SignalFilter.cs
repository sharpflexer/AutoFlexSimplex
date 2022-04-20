using AF_RobotService.Models.Interfaces;
using AF_RobotService.Models.Objects.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    public class SignalFilter
    {
        public static Dictionary<Signal, IStatus> signalToStatusConnections = new Dictionary<Signal, IStatus>()
        {
            { Signal.EnterLong, new StatusEnter()},
            { Signal.EnterShort, new StatusEnter()},
            { Signal.ExitLong, new StatusExitLong()},
            { Signal.ExitShort, new StatusExitShort()}
        };
        public static bool Check(Signal signal, IStatus currentStatus)
        {
            if (signal.Equals(Signal.Nothing)) return false;
            IStatus connectedStatus = signalToStatusConnections.GetValueOrDefault(signal);
            if (connectedStatus.Equals(currentStatus)) return true;
            else return false;
        }
    }
}
