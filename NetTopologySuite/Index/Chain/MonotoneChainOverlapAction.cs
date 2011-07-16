using System;
using GeoAPI.Coordinates;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NPack.Interfaces;

namespace NetTopologySuite.Index.Chain
{
    /// <summary> 
    /// The action for the internal iterator for performing
    /// overlap queries on a MonotoneChain.
    /// </summary>
    public class MonotoneChainOverlapAction<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
            IComputable<Double, TCoordinate>, IConvertible
    {
        private readonly IGeometryFactory<TCoordinate> _geoFactory;
        private readonly Extents<TCoordinate> _searchExtents1; // = new Extents<TCoordinate>();
        private readonly Extents<TCoordinate> _searchExtents2; // = new Extents<TCoordinate>();
        private LineSegment<TCoordinate> _overlapSeg1;
        private LineSegment<TCoordinate> _overlapSeg2;

        public MonotoneChainOverlapAction(IGeometryFactory<TCoordinate> geometryFactory)
        {
            _geoFactory = geometryFactory;
            _searchExtents1 = new Extents<TCoordinate>(_geoFactory);
            _searchExtents2 = new Extents<TCoordinate>(_geoFactory);
        }

        /// <summary>
        /// Gets one of the <see cref="IExtents{TCoordinate}"/>s 
        /// used during the MonotoneChain search process.
        /// </summary>
        public IExtents<TCoordinate> SearchExtents1
        {
            get { return _searchExtents1; }
        }

        /// <summary>
        /// Gets the other <see cref="IExtents{TCoordinate}"/>
        /// used during the MonotoneChain search process.
        /// </summary>
        public IExtents<TCoordinate> SearchExtents2
        {
            get { return _searchExtents2; }
        }

        /// <summary>
        /// This function can be overridden if the original chains are needed.
        /// </summary>
        /// <param name="start1">
        /// The index of the start of the overlapping segment from mc1.
        /// </param>
        /// <param name="start2">
        /// The index of the start of the overlapping segment from mc2.
        /// </param>
        public virtual void Overlap(MonotoneChain<TCoordinate> mc1, Int32 start1, MonotoneChain<TCoordinate> mc2,
                                    Int32 start2)
        {
            _overlapSeg1 = mc1.GetLineSegment(start1);
            _overlapSeg2 = mc2.GetLineSegment(start2);
            Overlap(_overlapSeg1, _overlapSeg2);
        }

        /// <summary> 
        /// This is a convenience function which can be overridden to obtain the actual
        /// line segments which overlap.
        /// </summary>
        public virtual void Overlap(LineSegment<TCoordinate> seg1, LineSegment<TCoordinate> seg2)
        {
        }
    }
}