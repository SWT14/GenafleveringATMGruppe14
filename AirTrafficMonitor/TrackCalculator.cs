using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor
{
    public class TrackCalculator : ITrackCalculator
    {
        public string tag { get; set; }
        public double X_coor { get; set; }
        public double Y_coor { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
        public DateTime timestamp { get; set; }

        public TrackCalculator(Track trackBefore, Track trackNow)
        {
            this.tag = trackNow.tag;
            X_coor = trackNow.X_coor;
            Y_coor = trackNow.Y_coor;
            this.Altitude = trackNow.Altitude;
            Velocity = VelocityCalculation(trackBefore.X_coor, X_coor, trackBefore.Y_coor,
                Y_coor, trackBefore.timestamp, timestamp);
            CompassCourse = CompassCourseCalculation(trackBefore.X_coor, X_coor, trackBefore.Y_coor,
                Y_coor);
        }

        public double VelocityCalculation(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2,
            DateTime timestamp1, DateTime timestamp2)
        {
            double difference = Span(X_coor1, X_coor2, Y_coor1, Y_coor2);
            TimeSpan timeSpan = timestamp1 - timestamp2;
            double timedifference = timeSpan.TotalMilliseconds / 1000;
            double velocity = difference / timedifference;
            return velocity;
        }

        public double Span(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2)
        {
            double difference = Math.Sqrt(Math.Pow((X_coor1 - X_coor2), 2) + Math.Pow((Y_coor1 - Y_coor2), 2));
            return difference;
        }

        public double CompassCourseCalculation(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2)
        {
            double X_difference = X_coor2 - X_coor1;
            double Y_difference = Y_coor2 - Y_coor1;

            double degrees = Math.Atan(X_difference / Y_difference) * 180 / Math.PI;

            if (X_difference > 0 && Y_difference < 0) 
                return -degrees + 90;

            if (X_difference < 0 && Y_difference > 0)
                return -degrees + 270; 
            
            if (X_difference < 0 && Y_difference < 0)
                return degrees + 180;
            
            if (X_difference == 0 && Y_difference < 0)
                return degrees + 180;

            if (X_difference < 0 && Y_difference == 0)
                return -degrees + 360;

            return degrees;
        }

    }
}
