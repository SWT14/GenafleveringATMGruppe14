using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirTrafficMonitor;
using NSubstitute;
using NUnit.Framework;
using AirTrafficMonitor.TransponderReceiver;


namespace AirTrafficMonitoring.Unit.Test
{
    [TestClass]
    public class TrackHandlerTest
    {
        private ITrackHandler _uut;
        private List<Track> tracklist;

        [SetUp]
        public void Setup()
        {
            ITransponderReceiver transponderreceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Splitter();
            transponderreceiver.TransponderDataReady += _uut.OnTransponderData;
            tracklist = new List<Track>();
        }


        [Test]
       public void CheckIfSinglePlaneOnly
        {
            var fakeplanes = new List<string>();
            fakeplanes.Add("ATR423;39045;12932;14000;20151006213456789");
            RawTransponderDataEventArgs RawTestData = new RawTransponderDataEventArgs(fakeplanes);
            _uut.OnTransponderData(null, Rawdata);
            assert.that(fakeplanes Has.Count.Equalto(1));
        }

        [Test]
       public void CheckIfSplitCorrectly
        {
            Track Track1 = new Track()
            {
                SingleTagSectionHandler =
            }
            tracklist.add(Track1);
        }
    }
}
