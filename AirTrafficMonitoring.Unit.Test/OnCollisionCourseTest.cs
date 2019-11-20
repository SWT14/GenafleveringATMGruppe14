using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AirTrafficMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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

        private TrackinAirEvent fakeTrackUpdated;
        private int trackEventCalled;
        private List<CollisionTracks> collisionList;
        private List<ITrackCalculator> trackCalculator;
        private IAirSpaceFilter ASF;
        

        [SetUp]
        public void Setup()
        {
            fakeTrackUpdated = new TrackinAirEvent();
            trackEventCalled = 0;
            collisionList = new List<CollisionTracks>();

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
            track4.Altitude = 400;
            track4.X_coor = 70000;
            track4.Y_coor = 30000;

            track5 = Substitute.For<ITrackCalculator>();
            track5.tag = "Flight 5";
            track5.Altitude = 699;
            track5.X_coor = 10000;
            track5.Y_coor = 12000;


            ASF = Substitute.For<IAirSpaceFilter>();

            _uut = new OnCollisionCourse(ASF);

            _uut.CreateSpan += (o, args) =>
            {
                collisionList = args.Collisiontracks;
                ++trackEventCalled;
            };
        }

        // Test 1 
        [Test]
        public void Track1And2_Positiv_Return()
        {
            double span = _uut.Span(track1, track2);
            Assert.That(span, Is.EqualTo(40000));
        }

        // Test 2
        [Test]
        public void Track1And2_Negativ_Return()
        {
            double span = _uut.Span(track2, track1);
            Assert.That(span, Is.EqualTo(40000));
        }

        // Test 3
        [Test] 
        public void onTrackUpdated_NoEvent_NoConflict()
        {
            Dictionary<String, ITrackCalculator> cTracks = new Dictionary<string, ITrackCalculator>();
            cTracks.Add(track3.tag,track3);
            cTracks.Add(track4.tag,track4);

            fakeTrackUpdated.tracks = cTracks;
            ASF.TrackUpdated += Raise.EventWith(fakeTrackUpdated);
            
            Assert.That(collisionList.Count, Is.EqualTo(0));
            Assert.That(trackEventCalled, Is.EqualTo(0));
        }

        //Test 4
        [Test]
        public void onTrackUpdated_EventRaised_Conflict()
        {
            Dictionary<String, ITrackCalculator> cTracks = new Dictionary<string, ITrackCalculator>();
            cTracks.Add(track3.tag, track3);
            cTracks.Add(track5.tag, track5);

            fakeTrackUpdated.tracks = cTracks;
            ASF.TrackUpdated += Raise.EventWith(fakeTrackUpdated);


            Assert.That(collisionList.Count, Is.EqualTo(1));
            Assert.That(trackEventCalled, Is.EqualTo(1));
            Assert.That(collisionList[0]._collisionFlights.Contains((track3)));
            Assert.That(collisionList[0]._collisionFlights.Contains((track5)));
        }


    }
}
