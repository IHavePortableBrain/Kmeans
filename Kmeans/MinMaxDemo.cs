using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kmeans
{
    internal class MinMaxDemo : IMinMax
    {
        private const Int32 maxClusterCount = 10;

        private readonly Random random = new Random();

        private readonly UInt32 kpointCount;
        private readonly UInt32 centroidCount;
        private readonly KPoint[] kPoints;
        private readonly Color[] clusterColor;
        private readonly List<Cluster> clusters;

        public KPoint[] KPoints => kPoints;

        public Centroid[] Centroids => clusters.ToArray().GetCentroids();

        public Cluster[] Clusters => clusters.ToArray();

        public MinMaxDemo(UInt32 kpointCount, UInt32 centroidCount, Int32 minX, Int32 maxX, Int32 minY, Int32 maxY)
        {
            if (centroidCount > maxClusterCount)
                throw new InvalidOperationException(String.Format("maxClusterCount is {0}", maxClusterCount));

            this.kpointCount = kpointCount;
            this.centroidCount = centroidCount;

            clusterColor = new Color[maxClusterCount] { Color.Red, Color.Orange, Color.Yellow, Color.Green,
                Color.Blue, Color.DarkBlue, Color.Violet, Color.Chocolate, Color.Crimson, Color.DarkCyan};

            kPoints = InitKPoints(minX, maxX, minY, maxY);

            clusters = InitClusters();
        }

        private List<Cluster> InitClusters()
        {
            var clusters = new List<Cluster>();

            var firstCluster = new Cluster(
                clusterColor[0],
                new Centroid(kPoints[random.Next(kPoints.Length)]),
                kPoints);

            clusters.Add(firstCluster);

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

        public Centroid[] EvaluateCentroids(out bool changed)
        {
            changed = false;

            double halfAvgInterCentroidsDistanse = clusters.ToArray().EvaluateCentroidsAvgDistance() / 2;

            double maxInnerDistance = clusters.Select(cluster => cluster.EvaluateClusterMaxInnerDistance()).ToArray().Max();

            if (maxInnerDistance > halfAvgInterCentroidsDistanse)
            {
                changed = true;
                //i know its bad but its lab
                var centroidOfNewCluster = new Centroid(clusters
                    .Find(cluster => cluster.MaxDistance.Equals(maxInnerDistance))
                    .MostDistantPoint);

                clusters.Add(new Cluster(clusterColor[clusters.Count], centroidOfNewCluster));
            }

            return clusters.ToArray().GetCentroids();
        }

        public KPoint[] ReClasterPoints()
        {
            foreach (var cluster in clusters)
            {
                cluster.ClearPoints();
            }

            clusters.ToArray().DoClasteringForPoints(KPoints);

            return KPoints;
        }
    }
}