using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kmeans
{
    internal struct KPoint
    {
        internal Point point;

        public KPoint(Point point)
        {
            this.point = point;
        }

        public double DistanceTo(KPoint other)
        {
            return Math.Sqrt(Math.Pow(other.point.X - this.point.X, 2) + Math.Pow(other.point.Y - this.point.Y, 2));
        }

        public static implicit operator Point(KPoint kPoint)
        {
            return kPoint.point;
        }
    }
}