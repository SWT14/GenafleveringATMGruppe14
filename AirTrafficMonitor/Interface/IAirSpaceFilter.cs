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
        string tag { get; set; }
        double X_coor { get; set; }
        double Y_coor { get; set; }
        double Altitude { get; set; }
        bool AirSpace { get; set; }
        bool FilterTracks(double X, double Y, double A);

        event EventHandler<TrackinAirEvent> TrackUpdated;
        public List<ITrack> trackList { get; set; }
        public Dictionary<string, ITrack> TrackDict { get; set; }
        public Dictionary<string, ITrackCalculator> TrackCalcDict { get; set; }
        void onTrackCreated(object s, TrackEvent Trackhandler);
        void Create(ITrack track);
        void Remove(string tag);

    }
}
