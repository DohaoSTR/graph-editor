using GraphModel.Assets.Model;
using GraphModel.Assets.Model.GraphElements;
using System.Drawing;
using System.Windows.Forms;

namespace Wave_Algorithm
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Pen blackPen;
        private Pen redPen;
        private Pen darkGoldPen;
        private Graphics graphics;
        private Font font;
        private Brush brush;
        private PointF pointf;
        private int R = 20; 

        private FieldGraph fieldGraph;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            blackPen = new Pen(Color.Black)
            {
                Width = 2
            };
            redPen = new Pen(Color.Red)
            {
                Width = 2
            };
            darkGoldPen = new Pen(Color.DarkGoldenrod)
            {
                Width = 2
            };
            font = new Font("Arial", 15);
            brush = Brushes.Black;

            fieldGraph = new FieldGraph();

            pictureBox1.Image = bitmap;
        }

        private void DrawVertex(Vertex vertex)
        {
            graphics.FillEllipse(Brushes.White, (point.X - R), (point.Y - R), 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, (point.X - R), (point.Y - R), 2 * R, 2 * R);
            pointf = new PointF(point.X - 9, point.Y - 9);
            graphics.DrawString(Vertex.GetVertices.Count.ToString(), font, brush, pointf);
        }

        private void DrawEdge()
        {
            
        }

        private void DrawLoop()
        { }

        private void button2_Click(object sender, System.EventArgs e) // вершина
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, System.EventArgs e) // Ребро
        {
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void button4_Click(object sender, System.EventArgs e) // удалить элемент
        {
            button4.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) // Нажатие на пичер бокс
        {
            if (button2.Enabled == false)
            {
                Vertex.GetVertices.Add(new Vertex(new GraphModel.Assets.Model.Point(e.X, e.Y)));
                DrawVertex(new GraphModel.Assets.Model.Point(e.X, e.Y));
                pictureBox1.Image = bitmap;
            }
        }

        private void DrawAllGraph()
        {
            //for (int i = 0; i < Edge.GetEdges.Count; i++)
            //{
            //    if (Edge.GetEdges[i].Second == Edge.GetEdges[i].First)
            //    {
            //        graphics.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
            //        pointf = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
            //    }
            //    else
            //    {
            //        graphics.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
            //        pointf = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
            //    }
            //}

            for (int i = 0; i < Vertex.GetVertices.Count; i++)
            {
                DrawVertex(Vertex.GetVertices[i].GetPoint);
            }
        }
    }
}
