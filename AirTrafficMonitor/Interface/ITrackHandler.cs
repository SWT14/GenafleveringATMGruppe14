using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitor
{
    public interface ITrackHandler
    {
        event EventHandler<TrackEvent> OnTrackCreated;
        void Rawhandler(string data);
    }
}