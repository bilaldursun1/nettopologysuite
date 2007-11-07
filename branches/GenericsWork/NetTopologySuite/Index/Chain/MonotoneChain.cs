using System;
using GeoAPI.Geometries;
using GisSharpBlog.NetTopologySuite.Geometries;

namespace GisSharpBlog.NetTopologySuite.Index.Chain
{
    /// <summary> 
    /// MonotoneChains are a way of partitioning the segments of a linestring to
    /// allow for fast searching of intersections.
    /// They have the following properties:
    /// the segments within a monotone chain will never intersect each other
    /// the envelope of any contiguous subset of the segments in a monotone chain
    /// is equal to the envelope of the endpoints of the subset.
    /// Property 1 means that there is no need to test pairs of segments from within
    /// the same monotone chain for intersection.
    /// Property 2 allows
    /// binary search to be used to find the intersection points of two monotone chains.
    /// For many types of real-world data, these properties eliminate a large number of
    /// segment comparisons, producing substantial speed gains.
    /// One of the goals of this implementation of MonotoneChains is to be
    /// as space and time efficient as possible. One design choice that aids this
    /// is that a MonotoneChain is based on a subarray of a list of points.
    /// This means that new arrays of points (potentially very large) do not
    /// have to be allocated.
    /// MonotoneChains support the following kinds of queries:
    /// Envelope select: determine all the segments in the chain which
    /// intersect a given envelope.
    /// Overlap: determine all the pairs of segments in two chains whose
    /// envelopes overlap.
    /// This implementation of MonotoneChains uses the concept of internal iterators
    /// to return the resultsets for the above queries.
    /// This has time and space advantages, since it
    /// is not necessary to build lists of instantiated objects to represent the segments
    /// returned by the query.
    /// However, it does mean that the queries are not thread-safe.
    /// </summary>
    public class MonotoneChain
    {
        private ICoordinate[] pts;
        private Int32 start, end;
        private IExtents env = null;
        private object context = null; // user-defined information
        private Int32 id; // useful for optimizing chain comparisons

        public MonotoneChain(ICoordinate[] pts, Int32 start, Int32 end, object context)
        {
            this.pts = pts;
            this.start = start;
            this.end = end;
            this.context = context;
        }

        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        public object Context
        {
            get { return context; }
        }

        public IExtents Envelope
        {
            get
            {
                if (env == null)
                {
                    ICoordinate p0 = pts[start];
                    ICoordinate p1 = pts[end];
                    env = new Extents(p0, p1);
                }

                return env;
            }
        }

        public Int32 StartIndex
        {
            get { return start; }
        }

        public Int32 EndIndex
        {
            get { return end; }
        }

        public void GetLineSegment(Int32 index, ref LineSegment ls)
        {
            ls.P0 = pts[index];
            ls.P1 = pts[index + 1];
        }

        /// <summary>
        /// Return the subsequence of coordinates forming this chain.
        /// Allocates a new array to hold the Coordinates.
        /// </summary>
        public ICoordinate[] Coordinates
        {
            get
            {
                ICoordinate[] coord = new ICoordinate[end - start + 1];
                Int32 index = 0;
                for (Int32 i = start; i <= end; i++)
                {
                    coord[index++] = pts[i];
                }
                return coord;
            }
        }

        /// <summary> 
        /// Determine all the line segments in the chain whose envelopes overlap
        /// the searchEnvelope, and process them.
        /// </summary>
        public void Select(IExtents searchEnv, MonotoneChainSelectAction mcs)
        {
            ComputeSelect(searchEnv, start, end, mcs);
        }

        private void ComputeSelect(IExtents searchEnv, Int32 start0, Int32 end0, MonotoneChainSelectAction mcs)
        {
            ICoordinate p0 = pts[start0];
            ICoordinate p1 = pts[end0];
            mcs.TempEnv1.Init(p0, p1);

            // terminating condition for the recursion
            if (end0 - start0 == 1)
            {
                mcs.Select(this, start0);
                return;
            }

            // nothing to do if the envelopes don't overlap
            if (!searchEnv.Intersects(mcs.TempEnv1))
            {
                return;
            }

            // the chains overlap, so split each in half and iterate  (binary search)
            Int32 mid = (start0 + end0)/2;

            // Assert: mid != start or end (since we checked above for end - start <= 1)
            // check terminating conditions before recursing
            if (start0 < mid)
            {
                ComputeSelect(searchEnv, start0, mid, mcs);
            }

            if (mid < end0)
            {
                ComputeSelect(searchEnv, mid, end0, mcs);
            }
        }

        public void ComputeOverlaps(MonotoneChain mc, MonotoneChainOverlapAction mco)
        {
            ComputeOverlaps(start, end, mc, mc.start, mc.end, mco);
        }

        private void ComputeOverlaps(Int32 start0, Int32 end0, MonotoneChain mc, Int32 start1, Int32 end1,
                                     MonotoneChainOverlapAction mco)
        {
            ICoordinate p00 = pts[start0];
            ICoordinate p01 = pts[end0];
            ICoordinate p10 = mc.pts[start1];
            ICoordinate p11 = mc.pts[end1];

            // terminating condition for the recursion
            if (end0 - start0 == 1 && end1 - start1 == 1)
            {
                mco.Overlap(this, start0, mc, start1);
                return;
            }

            // nothing to do if the envelopes of these chains don't overlap
            mco.TempEnv1.Init(p00, p01);
            mco.TempEnv2.Init(p10, p11);
            
            if (! mco.TempEnv1.Intersects(mco.TempEnv2))
            {
                return;
            }

            // the chains overlap, so split each in half and iterate  (binary search)
            Int32 mid0 = (start0 + end0)/2;
            Int32 mid1 = (start1 + end1)/2;

            // Assert: mid != start or end (since we checked above for end - start <= 1)
            // check terminating conditions before recursing
            if (start0 < mid0)
            {
                if (start1 < mid1)
                {
                    ComputeOverlaps(start0, mid0, mc, start1, mid1, mco);
                }

                if (mid1 < end1)
                {
                    ComputeOverlaps(start0, mid0, mc, mid1, end1, mco);
                }
            }

            if (mid0 < end0)
            {
                if (start1 < mid1)
                {
                    ComputeOverlaps(mid0, end0, mc, start1, mid1, mco);
                }

                if (mid1 < end1)
                {
                    ComputeOverlaps(mid0, end0, mc, mid1, end1, mco);
                }
            }
        }
    }
}