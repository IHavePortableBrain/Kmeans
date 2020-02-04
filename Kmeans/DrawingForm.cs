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
        private const Int32 PBSize = 800;

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

        private async void DrawingForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            bitmap = new Bitmap(pb.Width, pb.Height);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(Color.Black);

            IKmean demo = new KMeanDemo(200000, 7, 0, pb.Width, 0, pb.Height);

            bool changed = true;
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

                Task<bool> task = new Task<bool>(() =>
                {
                    demo.ReEvaluateCentroids(out changed);
                    demo.ReClasterPoints();
                    return changed;
                });

                task.Start();
                if (await task)
                    graphics.Clear(pb.BackColor);
            } while (changed);
        }
    }
}