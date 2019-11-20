using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;
using AirTrafficMonitor;

namespace AirTrafficMonitoring.Unit.Test
{
    [TestFixture]
    public class LogCollisionTest
    {
        private LogCollision log;
        private ITrackCalculator track1;
        private ITrackCalculator track2;
        private ITrackCalculator track3;
        private ITrackCalculator track4;

        [SetUp]
        public void SetUp()
        {
            IOnCollisionCourse LogCollision = Substitute.For<IOnCollisionCourse>();
            log = new LogCollision(LogCollision);

            track1 = Substitute.For<ITrackCalculator>();
            

            track2 = Substitute.For<ITrackCalculator>();
          

            track3 = Substitute.For<ITrackCalculator>();
            track3.tag = "CCCC";
            track3.X_coor = 50000;
            track3.Y_coor = 40000;
            track3.Altitude = 1000;
            DateTime dtime3 = new DateTime(20191120130035125);
            track3.timestamp = dtime3;

            track4 = Substitute.For<ITrackCalculator>();
            track4.tag = "ABCD";
            track4.X_coor = 10500.25;
            track4.Y_coor = 60000.25;
            track4.Altitude = 1000;
            DateTime dtime4 = new DateTime(20191120130035125);
            track4.timestamp = dtime4;
        }

        //Test 1
        [Test]
        public void CreateString_conflict_ContainsConflict()
        {
            List<CollisionTracks> collist = new List<CollisionTracks>();
            List<ITrackCalculator> trackcalclist = new List<ITrackCalculator>();

            track1.tag = "AAAA";
            track1.X_coor = 100;
            track1.Y_coor = 70;
            track1.Altitude = 89;
            DateTime dtime1 = new DateTime(20191120130035125);
            track1.timestamp = dtime1;

            track2.tag = "BBBB";
            track2.X_coor = 788;
            track2.Y_coor = 220;
            track2.Altitude = 300;
            DateTime dtime2 = new DateTime(20191120130035125);
            track2.timestamp = dtime2;

            trackcalclist.Add(track1);
            trackcalclist.Add(track2);

            CollisionTracks coltrack = new CollisionTracks(trackcalclist);
            string ddd = coltrack._collisionTime + ":" + coltrack._collisionTime;

            collist.Add(coltrack);

            string[] stringtrack = log.CreateString(collist);

            //Assert that string contains conflict
            Assert.That(stringtrack[0].Contains("AAAA"));
            Assert.That(stringtrack[0].Contains("BBBB"));

        }
    }
}
