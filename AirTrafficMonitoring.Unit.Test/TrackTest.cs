using System;
using System.Text;
using System.Collections.Generic;
using AirTrafficMonitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;
    

namespace AirTrafficMonitoring.Unit.Test
{
    
    [TestFixture]
    public class TrackTest
    {
        private ITrack track1;
        private ITrack track2;

        [SetUp]
        public void SetUp()
        {
            track1 = Substitute.For<ITrack>();
            track2 = Substitute.For<ITrack>();
        }

        // Test 1 - Track is in AirSpace using Boundary Value Analysis
        [TestCase(90000, 10000, 20000)]
        [TestCase(10000, 90000, 500)]
        [TestCase(50000, 70000, 10000)]
        [TestCase(20000, 40000, 8000)]
        public void BoundaryValueCordinates_TrackInAirSpace_ReturnTrue(double X, double Y, double A )
        {
            track1.X_coor = X;
            track1.Y_coor = Y;
            track1.Altitude = A;

            Track track = new Track("Flight 1", X, Y, A);

            Assert.That(track.Airspace, Is.EqualTo(true));
        }

        // Test 2 - Track is outside AirSpace using Boundary Value Analysis
        [TestCase(90001, 10000, 500)]
        [TestCase(10000, 90001, 500)]
        [TestCase(10000, 10000, 20001)]
        [TestCase(9999, 90000, 20000)]
        [TestCase(90000, 9999, 20000)]
        [TestCase(90000, 90000, 499)]
        public void BoundaryValueCordinates_TrackInAirSpace_ReturnFalse(double X, double Y, double A)
        {
            track2.X_coor = X;
            track2.Y_coor = Y;
            track2.Altitude = A;

            Track track = new Track("Flight 1", X, Y, A);

            Assert.That(track.Airspace, Is.EqualTo(false));
        }
    }
}
