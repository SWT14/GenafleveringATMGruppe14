using System;
using System.Collections.Generic;
using AirTrafficMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class OnCollisionCourseTest
    {
        private OnCollisionCourse _uut;
        private ITrackCalculator track1;
        private ITrackCalculator track2;
        private ITrackCalculator track3;
        private ITrackCalculator track4;
        private ITrackCalculator track5;
        private ITrackCalculator track6;
        private ITrackCalculator track7;
        private TrackinAirEvent fakeTrackUpdated;
        private int trackEventCalled;
        private List<OnCollisionCourse> collisionList;
        private List<ITrackCalculator> trackCalculator;

        [SetUp]
        public void Setup()
        {
            fakeTrackUpdated = new TrackinAirEvent();
            trackEventCalled = 0;
            collisionList = new List<OnCollisionCourse>();

            track1 = Substitute.For<ITrackCalculator>();
            track1.X_coor = 40000;
            track1.Y_coor = 0;

            track2 = Substitute.For<ITrackCalculator>();
            track2.X_coor = 0;
            track2.Y_coor = 0;

            track3 = Substitute.For<ITrackCalculator>();
            track3.tag = "Flight 3";
            track3.Altitude = 400;
            track3.X_coor = 10000;
            track3.Y_coor = 10000;

            track4 = Substitute.For<ITrackCalculator>();
            track4.tag = "Flight 4";
            track4.Altitude = 700;
            track4.X_coor = 10000;
            track4.Y_coor = 12000;

            track5 = Substitute.For<ITrackCalculator>();
            track5.tag = "Flight 5";
            track5.Altitude = 701;
            track5.X_coor = 10000;
            track5.Y_coor = 12000;

            track6 = Substitute.For<ITrackCalculator>();
            track6.tag = "Flight 6";
            track6.Altitude = 900;
            track6.X_coor = 10000;
            track6.Y_coor = 16000;

            track7 = Substitute.For<ITrackCalculator>();
            track7.tag = "Flight 7";
            track7.Altitude = 400;
            track7.X_coor = 70000;
            track7.Y_coor = 30000;

            trackCalculator = Substitute.For<IAirSpaceFilter>();

            _uut = new OnCollisionCourse(trackCalculator);

            _uut.CreateSpan += (o, args) =>
            {
                collisionList = args.Collisiontracks;
                ++trackEventCalled;
            };
        }


        [Test]
        public void Track1And2_Positiv_Return()
        {
            double span = _uut.Span(track1, track2);
            Assert.That(span, Is.EqualTo(40000));
        }
    }
}
