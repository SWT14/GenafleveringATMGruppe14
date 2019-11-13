using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor
{
    public class OnCollisionCourse : IOnCollisionCourse
    {
        public List<ITrackCalculator> _collisionFlights { get; } 
        public DateTime _collisionTime { get; }

        
        private List<OnCollisionCourse> _collisionTracks;
        public event EventHandler<SeperationEvent> CreateSeperation;
        public Dictionary<String, TrackCalculator> _tracks { get; set; }
        

        public OnCollisionCourse(List<ITrackCalculator> collisionFlights, IAirSpaceFilter airSpaceFilter)
        {
            _collisionFlights = collisionFlights;
            _collisionTime = DateTime.Now;

            airSpaceFilter.TrackUpdated += onTrackUpdated;
            _collisionTracks = new List<OnCollisionCourse>();
        }

        public void TrackInAirSpace(TrackinAirEvent TEtracks)
        {
            _tracks= TEtracks;
            _collisionTracks = new List<OnCollisionCourse>();

            List<ITrackCalculator> flight7 = new List<ITrackCalculator>();

            foreach (var track in TEtracks)
            {
                flight7.Add(track.Value);
            }

            for (int i = 0; i < flight7.Count; f++)
            {
                for (int f = i + 1; f < flight7.Count; f++)
                {
                    if (Span(flight7[i], flight7[f]) < 5000)
                    {
                        if (Math.Abs(flight7[i].Altitude - flight7[f].Altitude) < 300)
                        {
                            List<ITrackCalculator> cList = new List<ITrackCalculator>();
                            cList.Add(flight7[i]);
                            cList.Add(flight7[f]);
                            _collisionTracks.Add(new OnCollisionCourse(cList));
                        }
                    }
                }
            }

            if (_collisionTracks.Count > 0)
            {
                OnCreateSeperation(_collisionTracks);
            }
        }

        public double Span(ITrackCalculator track1, ITrackCalculator track2)
        {
            double difference1 = Math.Sqrt(Math.Pow((track1.X_coor - track2.X_coor), 2) + Math.Pow((track1.Y_coor - track2.Y_coor), 2));
            return Math.Abs(difference1);
        }

        protected virtual void OnCreateSeperation(List<OnCollisionCourse> collisiontracks)
        {
            CreateSeperation?.Invoke(this, new SeperationEvent() {Collisiontracks = collisiontracks});
        }

    }


    public class SeperationEvent : EventArgs
    {
        public List<OnCollisionCourse> Collisiontracks { get; set; }
    }
}
