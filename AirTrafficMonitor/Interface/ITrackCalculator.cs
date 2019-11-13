using System;

namespace AirTrafficMonitor
{
    public interface ITrackCalculator
    {
        string tag { get; set; }
        double X_coor { get; set; }
        double Y_coor { get; set; }
        double Altitude { get; set; }
        double Velocity { get; set; }
        double CompassCourse { get; set; }
        DateTime timestamp { get; set; }

        double VelocityCalculation(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2,
            DateTime timestamp1, DateTime timestamp2);

        double Span(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2);

        double CompassCourseCalculation(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2);
    }
}