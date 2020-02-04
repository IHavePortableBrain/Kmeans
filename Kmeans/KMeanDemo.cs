using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kmeans
{
    internal class KMeanDemo : IKmean
    {
        private const Int32 maxClusterCount = 7;

        private readonly Random random = new Random();

        private readonly UInt32 kpointCount;
        private readonly UInt32 centroidCount;
        private readonly KPoint[] kPoints;
        private readonly Color[] clusterColor;
        private readonly Cluster[] clusters;

        public KPoint[] KPoints => kPoints;

        public Centroid[] Centroids => clusters.GetCentroids();

        public Cluster[] Clusters => clusters;

        public KMeanDemo(UInt32 kpointCount, UInt32 centroidCount, Int32 minX, Int32 maxX, Int32 minY, Int32 maxY)
        {
            if (centroidCount > maxClusterCount)
                throw new InvalidOperationException(String.Format("maxClusterCount is {0}", maxClusterCount));

            this.kpointCount = kpointCount;
            this.centroidCount = centroidCount;

            clusterColor = new Color[maxClusterCount] { Color.Black, Color.Beige, Color.Blue, Color.Brown,
                Color.DeepPink, Color.ForestGreen, Color.Gray };

            clusters = InitClusters(minX, maxX, minY, maxY);

            kPoints = InitKPoints(minX, maxX, minY, maxY);

            clusters.DoClasteringForPoints(kPoints);
        }

        private Cluster[] InitClusters(Int32 minX, Int32 maxX, Int32 minY, Int32 maxY)
        {
            var clusters = new Cluster[centroidCount];

            for (int i = 0; i < centroidCount; i++)
            {
                clusters[i] = new Cluster(clusterColor[i],
                    new Centroid(
                        new Point(
                            random.Next(minX, maxX),
                            random.Next(minY, maxY))
                        ));
            }

            return clusters;
        }

        private KPoint[] InitKPoints(int minX, int maxX, int minY, int maxY)
        {
            var kPoints = new KPoint[kpointCount];
            for (int i = 0; i < kpointCount; i++)
            {
                kPoints[i] = new KPoint(
                    new Point(random.Next(minX, maxX), random.Next(minY, maxY)));
            }

            return kPoints;
        }

        public Centroid[] ReEvaluateCentroids(out bool changed)
        {
            changed = false;

            foreach (var cluster in clusters)
            {
                cluster.EvaluateNewCetroid(out changed); //even if one centroid changed out parametr is true
            }

            return clusters.GetCentroids();
        }

        public KPoint[] ReClasterPoints()
        {
            foreach (var cluster in clusters)
            {
                cluster.ClearPoints();
            }

            clusters.DoClasteringForPoints(KPoints);

            return KPoints;
        }
    }
}