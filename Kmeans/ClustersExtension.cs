using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static internal double EvaluateCentroidsAvgDistance(this Cluster[] clusters)
        {
            double result = 0f;

            Centroid[] centroids = clusters.GetCentroids();
            for (int i = 0; i < clusters.Length - 1; i++)
                result += new KPoint(centroids[i].point).DistanceTo(new KPoint(centroids[i + 1].point));

            if (clusters.Length > 1)
                result = result / (double)(clusters.Length - 1);

            return result;
        }
    }
}