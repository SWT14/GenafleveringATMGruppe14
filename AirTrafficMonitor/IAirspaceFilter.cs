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
        Dictionary<string, ITrackCalculator> TrackDict;
        Dictionary<string, ITrack> TrackDict;
        event EventHandler<TrackinAirEvent> TrackUpdated;
        List<ITrack> tracklist;
        void Create(ITrack track);
        void Remove(string tag);
    }
}
