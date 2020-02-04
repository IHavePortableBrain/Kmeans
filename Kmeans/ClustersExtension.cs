using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

namespace Kmeans
{
    public static class ClustersExtension
    {
        static internal Centroid[] GetCentroids(this Cluster[] clusters)
        {
            return clusters.Select(cluster => cluster.Centroid)?.ToArray();
        }

        static internal void DoClasteringForPoints(this Cluster[] clusters, KPoint[] kPoints)
        {
            Centroid[] centroids = clusters.GetCentroids();
            if (centroids == null)
                throw new InvalidOperationException("clusters have no centroids specified");

            foreach (var kPoint in kPoints)
            {
                double minDistance = double.MaxValue; //rewrite? with minDistance = distance to first cluster
                Cluster hostCluster = null;
                foreach (var claster in clusters)
                {
                    double distance = kPoint.DistanceTo((KPoint)claster.Centroid);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        hostCluster = claster;
                    }
                }
                hostCluster.AddKPoint(kPoint);
            }
        }
    }
}