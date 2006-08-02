using System;

using GisSharpBlog.NetTopologySuite.Geometries;

namespace GisSharpBlog.NetTopologySuite.Operation.Predicate
{
    /// <summary>
    /// Optimized implementation of spatial predicate "contains"
    /// for cases where the first <c>Geometry</c> is a rectangle.    
    /// As a further optimization,
    /// this class can be used directly to test many geometries against a single rectangle.
    /// </summary>
    public class RectangleContains
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Contains(Polygon rectangle, Geometry b)
        {
            RectangleContains rc = new RectangleContains(rectangle);
            return rc.Contains(b);
        }

        private Polygon rectangle;
        private Envelope rectEnv;

        /// <summary>
        /// Create a new contains computer for two geometries.
        /// </summary>
        /// <param name="rectangle">A rectangular geometry.</param>
        public RectangleContains(Polygon rectangle)
        {
            this.rectangle = rectangle;
            rectEnv = rectangle.EnvelopeInternal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geom"></param>
        /// <returns></returns>
        public bool Contains(Geometry geom)
        {
            if (!rectEnv.Contains(geom.EnvelopeInternal))
                return false;
            // check that geom is not contained entirely in the rectangle boundary
            if (IsContainedInBoundary(geom))
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geom"></param>
        /// <returns></returns>
        private bool IsContainedInBoundary(Geometry geom)
        {
            // polygons can never be wholely contained in the boundary
            if (geom is Polygon) 
                return false;
            if (geom is Point) 
                return IsPointContainedInBoundary((Point) geom);
            if (geom is LineString) 
                return IsLineStringContainedInBoundary((LineString) geom);

            for (int i = 0; i < geom.NumGeometries; i++) 
            {
                Geometry comp = geom.GetGeometryN(i);
                if (!IsContainedInBoundary(comp))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool IsPointContainedInBoundary(Point point)
        {
            return IsPointContainedInBoundary(point.Coordinate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool IsPointContainedInBoundary(Coordinate pt)
        {
            // we already know that the point is contained in the rectangle envelope
            if (!(pt.X == rectEnv.MinX || pt.X == rectEnv.MaxX))
                return false;
            if (!(pt.Y == rectEnv.MinY || pt.Y == rectEnv.MaxY))
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsLineStringContainedInBoundary(LineString line)
        {
            ICoordinateSequence seq = line.CoordinateSequence;
            Coordinate p0 = new Coordinate();
            Coordinate p1 = new Coordinate();
            for (int i = 0; i < seq.Count - 1; i++)
            {
                seq.GetCoordinate(i, p0);
                seq.GetCoordinate(i + 1, p1);
                if (!IsLineSegmentContainedInBoundary(p0, p1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        private bool IsLineSegmentContainedInBoundary(Coordinate p0, Coordinate p1)
        {
            if (p0.Equals(p1))
                return IsPointContainedInBoundary(p0);
            // we already know that the segment is contained in the rectangle envelope
            if (p0.X == p1.X)
            {
                if (p0.X == rectEnv.MinX || 
                    p0.X == rectEnv.MaxX)
                        return true;
            }
            else if (p0.Y == p1.Y)
            {
                if (p0.Y == rectEnv.MinY || 
                    p0.Y == rectEnv.MaxY)
                        return true;
            }
            /*
             * Either both x and y values are different
             * or one of x and y are the same, but the other ordinate is not the same as a boundary ordinate
             * In either case, the segment is not wholely in the boundary
             */
            return false;         
        }
    }
}
