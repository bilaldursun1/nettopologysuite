using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Wintellect.PowerCollections;

namespace NetTopologySuite.Triangulate
{
    /// <summary>
    /// Creates a map between the vertex <see cref="Coordinate"/>s of a 
    /// set of <see cref="Geometry"/>s,
    /// and the parent geometry, and transfers the source geometry
    /// data objects to geometry components tagged with the coordinates.
    /// </summary>
    /// <remarks>
    /// This class can be used in conjunction with <see cref="VoronoiDiagramBuilder"/>
    /// to transfer data objects from the input site geometries
    /// to the constructed Voronou polygons.
    /// </remarks>
    /// <author>Martin Davis</author>
    /// <see cref="VoronoiDiagramBuilder"/>
    public class VertexTaggedGeometryDataMapper
    {
        private IDictionary<ICoordinate, object> coordDataMap = new OrderedDictionary<ICoordinate, object>();

        public VertexTaggedGeometryDataMapper()
        {
        }

        public void LoadSourceGeometries(ICollection<IGeometry> geoms)
        {
            foreach (var geom in geoms)
            {
                LoadVertices(geom.Coordinates, geom.UserData);
            }
        }

        public void LoadSourceGeometries(IGeometryCollection geomColl)
        {
            for (int i = 0; i < geomColl.NumGeometries; i++)
            {
                IGeometry geom = geomColl.GetGeometryN(i);
                LoadVertices(geom.Coordinates, geom.UserData);
            }
        }

        private void LoadVertices(ICoordinate[] pts, object data)
        {
            for (int i = 0; i < pts.Length; i++)
            {
                coordDataMap.Add(pts[i], data);
            }
        }

        public IList<ICoordinate> Coordinates
        {
            get
            {
                return new List<ICoordinate>(coordDataMap.Keys);
            }
        }

        /// <summary>
        /// Input is assumed to be a multiGeometry
        /// in which every component has its userData
        /// set to be a Coordinate which is the key to the output data.
        /// The Coordinate is used to determine
        /// the output data object to be written back into the component. 
        /// </summary>
        /// <param name="targetGeom" />
        public void TransferData(IGeometry targetGeom)
        {
            for (int i = 0; i < targetGeom.NumGeometries; i++)
            {
                var geom = targetGeom.GetGeometryN(i);
                var vertexKey = (ICoordinate)geom.UserData;
                if (vertexKey == null) continue;
                geom.UserData = coordDataMap[vertexKey];
            }
        }
    }
}