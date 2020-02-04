using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kmeans
{
    public partial class DrawingForm : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen pen;
        private Brush brush = new SolidBrush(Color.OrangeRed);

        public DrawingForm()
        {
            InitializeComponent();
        }

        private void DrawingForm_Shown(object sender, EventArgs e)
        {
        }

        private void DrawingForm_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void DrawingForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            bitmap = new Bitmap(pb.Width, pb.Height);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(Color.Black);

            IKmean demo = new KMeanDemo(100000, 5, 0, pb.Width, 0, pb.Height);

            bool changed;
            do
            {
                foreach (var cluster in demo.Clusters)
                {
                    pen.Color = cluster.Color;
                    var rectangles = cluster.KPoints.Select(kpoint => new Rectangle(kpoint.point, new Size(1, 1)));

                    graphics.DrawRectangles(pen, rectangles.ToArray());

                    graphics.FillEllipse(brush, new Rectangle(cluster.Centroid.point, new Size(10, 10)));
                }
                pb.Image = bitmap;
                pb.Invalidate();

                demo.ReEvaluateCentroids(out changed);
                demo.ReClasterPoints();

                if (changed)
                    graphics.Clear(pb.BackColor);
            } while (changed);
        }
    }
}