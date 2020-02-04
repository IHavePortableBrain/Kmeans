using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kmeans
{
    internal interface IKmean
    {
        KPoint[] KPoints { get; }
        Centroid[] Centroids { get; }
        Cluster[] Clusters { get; }

        Centroid[] ReEvaluateCentroids(out bool changed);

        KPoint[] ReClasterPoints();
    }
}