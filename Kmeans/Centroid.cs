using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kmeans
{
    internal class Centroid
    {
        internal Point point;

        public Centroid() : this(new Point())
        {
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Centroid other))
                return false;

            return this.point.Equals(other.point);
        }

        public Centroid(Point point)
        {
            this.point = point;
        }

        public static implicit operator KPoint(Centroid centroid)
        {
            return new KPoint(centroid.point);
        }
    }
}