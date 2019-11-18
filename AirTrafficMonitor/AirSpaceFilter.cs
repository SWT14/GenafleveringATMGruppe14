using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;




namespace AirTrafficMonitor
{
    public class AirSpaceFilter : IAirSpaceFilter
    {
      
        public Dictionary<string,ITrack> TrackDict { get; }
        public Dictionary<string,ITrackCalculator> TrackCalcDict { get; set; }

       public event EventHandler<TrackinAirEvent> TrackUpdated;
        public List<ITrack> trackList { get; set; }

        public AirSpaceFilter(ITrackHandler trackhandler)
        {
            //Attaching the event to the constructor
            trackhandler.OnTrackCreated += onTrackCreated;
            trackList = new List<ITrack>();
            TrackDict = new Dictionary<string, ITrack>();
            TrackCalcDict = new Dictionary<string, ITrackCalculator>();
        }

        public void onTrackCreated(object s, TrackEvent Trackhandler)
        {
            trackList = Trackhandler.tlist;
            foreach (ITrack track in trackList)
            {
                if (TrackDict.ContainsKey(track.tag))
                {
                    if (track.Airspace)
                    {
                        Create(track);
                    }
                    else
                    {
                        if (TrackDict[track.tag].Airspace)
                        {
                            Remove(track.tag);
                        }
                        else
                        {
                            TrackDict[track.tag] = track;
                        }
                    }
                }
                else
                {
                    TrackDict.Add(track.tag, track);
                }
            }
        }

        public void Create(ITrack track)
        {
            if (TrackCalcDict.ContainsKey(track.tag))
            {
                TrackCalcDict[track.tag] = new TrackCalculator(TrackDict[track.tag], track);
            }
            else
            {
                TrackCalcDict.Add(track.tag, new TrackCalculator(TrackDict[track.tag], track));
            }

            TrackDict[track.tag] = track;

            onTrackUpdated(TrackCalcDict);
        }

        public void Remove(string tag)
        {
            TrackDict.Remove(tag);
            TrackCalcDict.Remove(tag);

            onTrackUpdated(TrackCalcDict);
        }

        protected virtual void onTrackUpdated(Dictionary<string, ITrackCalculator> t)
        {
            TrackUpdated?.Invoke(this, new TrackinAirEvent() {tracks = t});
        }

    }

    public class TrackinAirEvent : EventArgs
    {
        public Dictionary<string, TrackCalculator> tracks { get; set; }
    }
}
