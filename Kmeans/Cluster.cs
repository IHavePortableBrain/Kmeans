﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kmeans
{
    internal class Cluster
    {
        private List<KPoint> kPoints;
        private Centroid centroid;
        private readonly Color color;
        private Point mostDistantPoint;
        private double maxDistance;

        internal Centroid Centroid => centroid;

        internal Color Color => color;

        internal List<KPoint> KPoints { get => kPoints; set => kPoints = value; }
        public Point MostDistantPoint { get => mostDistantPoint; set => mostDistantPoint = value; }
        public double MaxDistance { get => maxDistance; set => maxDistance = value; }

        public Cluster(Color color) : this(color, null, new KPoint[0])
        {
        }

        public Cluster(Color color, Centroid centroid) : this(color, centroid, new KPoint[0])
        {
        }

        public Cluster(Color color, Centroid centroid, KPoint[] kPoints)
        {
            this.centroid = centroid;
            this.color = color;
            this.kPoints = kPoints?.ToList();
        }

        internal void AddKPoint(KPoint kPoint)
        {
            kPoints?.Add(kPoint);
        }

        internal void ClearPoints()
        {
            kPoints.Clear();
        }

        internal Centroid EvaluateNewCetroid(out bool changed)
        {
            changed = false;
            var newCetroid = new Centroid();

            foreach (KPoint kPoint in kPoints)
            {
                newCetroid.point.X += kPoint.point.X; // be aware of overflow
                newCetroid.point.Y += kPoint.point.Y;
            }

            newCetroid.point.X /= kPoints.Count;
            newCetroid.point.Y /= kPoints.Count;

            changed = !newCetroid.Equals(centroid);
            centroid = newCetroid;

            return centroid;
        }

        internal double EvaluateClusterMaxInnerDistance()
        {
            maxDistance = double.MinValue; //rewrite? with minDistance = distance to first cluster

            foreach (var kPoint in kPoints)
            {
                double distance = kPoint.DistanceTo(this.centroid);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    mostDistantPoint = kPoint;
                }
            }

            return maxDistance;
        }
    }
}