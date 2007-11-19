using System;
using System.Collections;

namespace GisSharpBlog.NetTopologySuite.Index.Bintree
{
    /// <summary>
    /// An <c>BinTree</c> (or "Binary Interval Tree")
    /// is a 1-dimensional version of a quadtree.
    /// It indexes 1-dimensional intervals (which of course may
    /// be the projection of 2-D objects on an axis).
    /// It supports range searching
    /// (where the range may be a single point).
    /// This implementation does not require specifying the extent of the inserted
    /// items beforehand.  It will automatically expand to accomodate any extent
    /// of dataset.
    /// This index is different to the Interval Tree of Edelsbrunner
    /// or the Segment Tree of Bentley.
    /// </summary>
    public class Bintree
    {
        /// <summary>
        /// Ensure that the Interval for the inserted item has non-zero extents.
        /// Use the current minExtent to pad it, if necessary.
        /// </summary>
        public static Interval EnsureExtent(Interval itemInterval, Double minExtent)
        {
            Double min = itemInterval.Min;
            Double max = itemInterval.Max;

            // has a non-zero extent
            if (min != max)
            {
                return itemInterval;
            }

            // pad extent
            if (min == max)
            {
                min = min - minExtent/2.0;
                max = min + minExtent/2.0;
            }

            return new Interval(min, max);
        }

        private Root root;

        /*
        * Statistics:
        * minExtent is the minimum extent of all items
        * inserted into the tree so far. It is used as a heuristic value
        * to construct non-zero extents for features with zero extent.
        * Start with a non-zero extent, in case the first feature inserted has
        * a zero extent in both directions.  This value may be non-optimal, but
        * only one feature will be inserted with this value.
        **/
        private Double minExtent = 1.0;

        public Bintree()
        {
            root = new Root();
        }

        public Int32 Depth
        {
            get
            {
                if (root != null)
                {
                    return root.Depth;
                }

                return 0;
            }
        }

        public Int32 Count
        {
            get
            {
                if (root != null)
                {
                    return root.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Compute the total number of nodes in the tree.
        /// </summary>
        /// <returns>The number of nodes in the tree.</returns>
        public Int32 NodeSize
        {
            get
            {
                if (root != null)
                {
                    return root.NodeCount;
                }

                return 0;
            }
        }

        public void Insert(Interval itemInterval, object item)
        {
            CollectStats(itemInterval);
            Interval insertInterval = EnsureExtent(itemInterval, minExtent);
            root.Insert(insertInterval, item);
        }

        public IEnumerator GetEnumerator()
        {
            IList foundItems = new ArrayList();
            root.AddAllItems(foundItems);
            return foundItems.GetEnumerator();
        }

        public IList Query(Double x)
        {
            return Query(new Interval(x, x));
        }

        /// <summary>
        /// min and max may be the same value.
        /// </summary>
        public IList Query(Interval interval)
        {
            /*
             * the items that are matched are all items in intervals
             * which overlap the query interval
             */
            IList foundItems = new ArrayList();
            Query(interval, foundItems);
            return foundItems;
        }

        public void Query(Interval interval, IList foundItems)
        {
            root.AddAllItemsFromOverlapping(interval, foundItems);
        }

        private void CollectStats(Interval interval)
        {
            Double del = interval.Width;
            if (del < minExtent && del > 0.0)
            {
                minExtent = del;
            }
        }
    }
}