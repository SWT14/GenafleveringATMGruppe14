using System;
using AirTrafficMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class TrackCalculatorTest
    {
        private ITrack track1;
        private ITrack track2;

        [SetUp]
        public void SetUp()
        {
            track1 = Substitute.For<ITrack>();
            track2 = Substitute.For<ITrack>();
        }

        // Test method 1 - VelocityCalculation
        [Test]
        public void CompassCourse_Return_DateVelocity()
        {
            DateTime dateTime1 = DateTime.ParseExact("20191101220012345", "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dateTime2 = DateTime.ParseExact("20191101220023456", "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);

            track1.timestamp = dateTime1;
            track1.X_coor = 0;
            track1.Y_coor = 0;

            track2.timestamp = dateTime2;
            track2.X_coor = 100;
            track2.Y_coor = 200;

            TrackCalculator track = new TrackCalculator(track1, track2);
            double velocity = track.VelocityCalculation(400, 0, 300, 0, dateTime1, dateTime2);
            Assert.That(velocity, Is.EqualTo(500));

        }



        // Test method 2 - Span

        [TestCase(400, 0, 300, 0, 500)]
        [TestCase(200, 0, 0, 0, 200)]
        [TestCase(0, 100, 0, 0, 100)]
        [TestCase(0, 0, 500, 0, 500)]
        [TestCase(0, 0, 0, 6, 600)]
        public void CompassCourse_Return_Span(double X_coor1, double X_coor2, double Y_coor1, double Y_coor2,
            double span)
        {
            track1.X_coor = X_coor1;
            track2.X_coor = X_coor2;
            track1.Y_coor = Y_coor1;
            track2.Y_coor = Y_coor2;

            TrackCalculator track = new TrackCalculator(track1, track2);
            double difference = track.Span(X_coor1, X_coor2, Y_coor1, Y_coor2);
            Assert.That(difference, Is.EqualTo(span));

        }


    }
}
