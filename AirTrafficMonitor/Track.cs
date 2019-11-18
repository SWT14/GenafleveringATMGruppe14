using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitor
{
    public class Track : ITrack
    {
        public string tag { get; set; }
        public double X_coor { get; set; }
        public double Y_coor { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
        public DateTime timestamp { get; set; }
        public bool Airspace { get; set; }

        public Track(string Tag, double X_c, double Y_c, double Alt)
        {
            this.tag = Tag;
            this.X_coor = X_c;
            this.Y_coor = Y_c;
            this.Altitude = Alt;
            this.Airspace = FlightTrack(X_c, Y_c, Alt);
        }
        //public Track() { }
        public bool FlightTrack(double X_c, double Y_c, double Alt)
        {
            if (X_c >= 10000 && X_c <= 90000 && Y_c >= 10000 && Y_c <= 90000 && Alt >= 500 && Alt <= 20000)
            {
                return true;
            }
            return false;
        }
    }
}