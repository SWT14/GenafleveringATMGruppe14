using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirTrafficMonitor;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class AirSpaceFilterTest
    {
        private ITrack track1;
        private ITrack track2;
        private ITrack track3;
        private ITrack track4;

        private IAirSpaceFilter _uut;
        private ITrackHandler _trackhandler;
        private TrackEvent _fakeTrackHandlerEvent;
        private int CalledEvent;

        [SetUp]
        public void SetUp()
        {
            _trackhandler = Substitute.For<ITrackHandler>();
            _uut = new AirSpaceFilter(_trackhandler);
            _fakeTrackHandlerEvent = new TrackEvent();

            track1 = Substitute.For<ITrack>();
            track1.tag = "ABCD";
            track1.X_coor = 5000.50;
            track1.Y_coor = 3000.50;
            track1.Altitude = 1000.75;
            track1.timestamp = DateTime.Now;
            track1.Airspace = false;

            track2 = Substitute.For<ITrack>();
            track2.tag = "ABCD";
            track2.X_coor = 6500.50;
            track2.Y_coor = 5400.50;
            track2.Altitude = 1000.75;
            track2.timestamp = DateTime.Now;
            track2.Airspace = false;

            track3 = Substitute.For<ITrack>();
            track3.tag = "ABCD";
            track3.X_coor = 50000.50;
            track3.Y_coor = 40000.50;
            track3.Altitude = 10000.75;
            track3.timestamp = DateTime.Now;
            track3.Airspace = true;

            track4 = Substitute.For<ITrack>();
            track4.tag = "ABCD";
            track4.X_coor = 10500.25;
            track4.Y_coor = 60000.25;
            track4.Altitude = 15000.20;
            track4.timestamp = DateTime.Now;
            track4.Airspace = true;

            CalledEvent = 0;
            _uut.TrackUpdated += (o, args) => { ++CalledEvent; };
        }

        //Test 1
        [Test]
        public void onTrackCreated_RaiseEvent()
        {
            List<ITrack> tracklists = new List<ITrack>() {track1};
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that onTrackCreated event is called
            Assert.That(_uut.trackList, Is.EqualTo(tracklists));
        }

        //Test 2
        [Test]
        public void TrackCreated_EventNotRaised()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that event has not been raised
            Assert.That(CalledEvent, Is.EqualTo(0));
        }
        //Test 3
        [Test]
        public void CreatedTrack_TrackDicContainsTrack()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that TrackDictionary contains Track
            Assert.That(_uut.TrackDict.ContainsKey(track1.tag) && _uut.TrackDict.ContainsValue(track1));
        }

        //Test 4
        [Test]
        public void TwoTrackOnTrackinAirEvent_TrackDicUpdated()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1,track2 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that TrackDic contains updated track
            Assert.That(_uut.TrackDict.ContainsKey(track2.tag) && _uut.TrackDict.ContainsValue(track2));
        }

        //Test 5
        [Test]
        public void TwoTrackOnTrackinAirEvent_EventIsNotRaised()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1, track2 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that event is raised
            Assert.That(CalledEvent, Is.EqualTo(0));
        }

        //Test 6
        [Test]
        public void TrackinAirspace_TrackCalcDicContainsTrack()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1, track2, track3 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that TrackCalcDic contains track

            Assert.That(_uut.TrackCalcDict.ContainsKey(track3.tag));
            Assert.That(_uut.TrackCalcDict[track3.tag].X_coor, Is.EqualTo(track3.X_coor));
            Assert.That(_uut.TrackCalcDict[track3.tag].Y_coor, Is.EqualTo(track3.Y_coor));
        }

        //Test 7
        [Test]
        public void TrackinAirspace_TrackCalcDicContainsUpdatedTrack()
        {
            List<ITrack> tracklists = new List<ITrack>() { track1, track2, track3 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            List<ITrack> tracklists2 = new List<ITrack>() { track4 };
            _fakeTrackHandlerEvent.tlist = tracklists2;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that TrackCalcDic contains most recent track
            Assert.That(_uut.TrackCalcDict.ContainsKey(track4.tag));
            Assert.That(_uut.TrackCalcDict[track4.tag].X_coor, Is.EqualTo(track4.X_coor));
            Assert.That(_uut.TrackCalcDict[track4.tag].Y_coor, Is.EqualTo(track4.Y_coor));
        }

        //Test 8
        [Test]
        public void TrackinAirspace_EventIsRaised()
        {
            List<ITrack> tracklists = new List<ITrack>() {track1, track2, track3};
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            //Assert that event is raised
            Assert.That(CalledEvent, Is.EqualTo(1));
        }

        //Test 9
        [Test]
        public void TrackinAirspace_TrackOutOfAirspace_RemoveTrack()
        {
            List<ITrack> tracklists = new List<ITrack>() { track3 };
            _fakeTrackHandlerEvent.tlist = tracklists;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            List<ITrack> tracklists2 = new List<ITrack>() { track1 };
            _fakeTrackHandlerEvent.tlist = tracklists2;
            _trackhandler.OnTrackCreated += Raise.EventWith(_fakeTrackHandlerEvent);

            Assert.That(!_uut.TrackCalcDict.ContainsKey(track1.tag));
        }
    }
}
