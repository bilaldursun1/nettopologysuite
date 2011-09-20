using System;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NUnit.Framework;

namespace NetTopologySuite.Tests.NUnit.Geometries.Implementation
{
    [TestFixture]
    public class BasicCoordinateSequenceTest
    {
        [Test]
        public void TestClone()
        {
            ICoordinateSequence s1 = CoordinateArraySequenceFactory.Instance.Create(
                new Coordinate[] { new Coordinate(1, 2), new Coordinate(3, 4) });
            ICoordinateSequence s2 = (ICoordinateSequence)s1.Clone();
            Assert.IsTrue(s1.GetCoordinate(0).Equals(s2.GetCoordinate(0)), "Coordinates are not equal!");
            Assert.IsTrue(!ReferenceEquals(s1.GetCoordinate(0), s2.GetCoordinate(0)), "Coordinates are the same instances");
        }
    }
}
