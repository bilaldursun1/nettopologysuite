using System;
using System.IO;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GisSharpBlog.NetTopologySuite.GeometriesGraph
{
    /// <summary> 
    /// An EdgeIntersection represents a point on an
    /// edge which intersects with another edge.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The intersection may either be a single point, or a line segment
    /// (in which case this point is the start of the line segment)
    /// The label attached to this intersection point applies to
    /// the edge from this point forwards, until the next
    /// intersection or the end of the edge.
    /// </para>
    /// The intersection point must be precise.
    /// </remarks>
    public struct EdgeIntersection<TCoordinate> : IComparable<EdgeIntersection<TCoordinate>>
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>,
            IComputable<Double, TCoordinate>, IConvertible
    {
        private readonly TCoordinate _coordinate;
        private readonly Double _distance;
        private readonly Int32 _segmentIndex;

        /// <summary>
        /// The point of intersection.
        /// </summary>
        public TCoordinate Coordinate
        {
            get { return _coordinate; }
        }

        /// <summary>
        /// The index of the containing line segment in the parent edge.
        /// </summary>
        public Int32 SegmentIndex
        {
            get { return _segmentIndex; }
        }

        /// <summary>
        /// The edge distance of this point along the containing line segment.
        /// </summary>
        public Double Distance
        {
            get { return _distance; }
        }

        public EdgeIntersection(TCoordinate coord, Int32 segmentIndex, Double distance)
        {
            _coordinate = coord;
            _segmentIndex = segmentIndex;
            _distance = distance;
        }

        public Int32 CompareTo(EdgeIntersection<TCoordinate> other)
        {
            return Compare(other.SegmentIndex, other.Distance);
        }

        /// <returns>
        /// -1 this EdgeIntersection is located before the argument location,
        /// 0 this EdgeIntersection is at the argument location,
        /// 1 this EdgeIntersection is located after the argument location.
        /// </returns>
        public Int32 Compare(Int32 segmentIndex, Double distance)
        {
            if (SegmentIndex < segmentIndex)
            {
                return -1;
            }

            if (SegmentIndex > segmentIndex)
            {
                return 1;
            }

            if (Distance < distance)
            {
                return -1;
            }

            if (Distance > distance)
            {
                return 1;
            }

            return 0;
        }

        public Boolean IsEndPoint(Int32 maxSegmentIndex)
        {
            if (SegmentIndex == 0 && Distance == 0.0)
            {
                return true;
            }

            if (SegmentIndex == maxSegmentIndex)
            {
                return true;
            }

            return false;
        }

        public void Write(StreamWriter outstream)
        {
            outstream.Write(Coordinate);
            outstream.Write(" seg # = " + SegmentIndex);
            outstream.WriteLine(" dist = " + Distance);
        }
    }
}