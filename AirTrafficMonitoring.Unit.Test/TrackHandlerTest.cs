using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirTrafficMonitor;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;
using TransponderReceiver;


namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class TrackHandlerTest
    {
        private TrackHandler _uut;
        private List<Track> tracklist;

        [SetUp]
        public void Setup()
        {
            ITransponderReceiver transponderreceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackHandler(transponderreceiver);
            transponderreceiver.TransponderDataReady += _uut.DataHandler;
            tracklist = new List<Track>();
        }


        [Test]
       public void CheckIfSinglePlaneOnly()  // tjekker om den forstår at skelne mellem fly
        {
            var fakeplanes = new List<string>();
            fakeplanes.Add("ATR423;39045;12932;14000;20151006213456789");
            RawTransponderDataEventArgs RawTestData = new RawTransponderDataEventArgs(fakeplanes);
            _uut.DataHandler(null, RawTestData);
            Assert.That(fakeplanes, Has.Count.EqualTo(1));
        }

        [Test]
       public void CheckIfSplitCorrectly() // tjekker om rawhandler splitter tracket rigtigt ved at sammenligne med prædifineret track
        {
            Track Track1 = new Track("DOH322", 23000, 34023, 7600); // skabelon      
            tracklist.Add(Track1);
            var fakeplane = new List<string>();
            fakeplane.Add("DOH322;23000;34023;7600");
            RawTransponderDataEventArgs RawTestData = new RawTransponderDataEventArgs(fakeplane);
            _uut.DataHandler(null, RawTestData);
            Assert.That(fakeplane, Is.EqualTo(Track1));
        }
    }
}
