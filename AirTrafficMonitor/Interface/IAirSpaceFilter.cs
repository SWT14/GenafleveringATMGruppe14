using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor;

namespace AirTrafficMonitor
{
    public interface IAirSpaceFilter
    {
        event EventHandler<TrackinAirEvent> TrackUpdated;
        List<ITrack> trackList { get; set; }
        Dictionary<string, ITrack> TrackDict { get; }
        Dictionary<string, ITrackCalculator> TrackCalcDict { get; }
        void onTrackCreated(object s, TrackEvent Trackhandler);
        void Create(ITrack track);
        void Remove(string tag);

    }
}
