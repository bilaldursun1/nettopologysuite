using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GisSharpBlog.NetTopologySuite.GeometriesGraph.Index
{
    public class MonotoneChain<TCoordinate>
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
    {
        private readonly MonotoneChainEdge<TCoordinate> _monotoneChainEdge;
        private readonly Int32 _chainIndex;

        public MonotoneChain(MonotoneChainEdge<TCoordinate> monotoneChainEdge, 
                             Int32 chainIndex)
        {
            _monotoneChainEdge = monotoneChainEdge;
            _chainIndex = chainIndex;
        }

        public void ComputeIntersections(MonotoneChain<TCoordinate> monotoneChain, 
                                         SegmentIntersector<TCoordinate> si)
        {
            _monotoneChainEdge.ComputeIntersectsForChain(_chainIndex, 
                                                         monotoneChain._monotoneChainEdge, 
                                                         monotoneChain._chainIndex, 
                                                         si);
        }
    }
}