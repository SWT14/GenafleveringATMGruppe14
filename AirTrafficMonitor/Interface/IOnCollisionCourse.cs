using System;
using System.Collections.Generic;

namespace AirTrafficMonitor
{
    public interface IOnCollisionCourse
    {
        event EventHandler<SpanEvent> CreateSpan;

        double Span(ITrackCalculator track1, ITrackCalculator track2);
        void onTrackUpdated(object source, TrackinAirEvent TEtracks); 
        Dictionary<string, ITrackCalculator> _tracks { get; set; }
        
    }
}