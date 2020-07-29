using GraphModel.Assets.Model;
using GraphModel.Assets.Model.GraphElements;
using System;
using System.Drawing;
using System.Windows.Forms;
using Point = GraphModel.Assets.Model.Point;

namespace Wave_Algorithm
{
    public enum SelectedVertex
    {
        None, First, Second
    }

    public partial class Form1 : Form
    {
        private SelectedVertex firstVertex;
        private SelectedVertex secondVertex;

        private int numberOfSelectedFirstVertex;
        private int numberOfSelectedSecondVertex;

        private readonly Bitmap bitmap;
        private readonly Pen blackPen;
        private readonly Pen redPen;
        private readonly Pen darkGoldPen;
        private readonly Graphics graphics;
        private readonly Font font;
        private readonly Brush brush;
        private PointF pointf;
        private readonly int R = 20; 

        private readonly FieldGraph fieldGraph;

        private Vertex _first;
        private Vertex _Second;

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

            firstVertex = SelectedVertex.None;
            secondVertex = SelectedVertex.None;

            fieldGraph = new FieldGraph();

            pictureBox1.Image = bitmap;
        }

        private int GetNumberOfVertex(Vertex vertex)
        {
            int i = 0;
            foreach (Vertex element in Vertex.Vertices)
            {
                i++;
                if (element == vertex)
                {
                    return i;
                }
            }
            throw new Exception("Вершина не найдена!");
        }

        private void DrawVertex(Vertex vertex)
        {
            graphics.FillEllipse(Brushes.White, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            pointf = new PointF(vertex.GetPoint.X - 9, vertex.GetPoint.Y - 9);
            graphics.DrawString(GetNumberOfVertex(vertex).ToString(), font, brush, pointf);
        }

        private void DrawEdge(Edge edge)
        {
            graphics.DrawLine(darkGoldPen, edge.First.GetPoint.X, edge.First.GetPoint.Y, edge.Second.GetPoint.X, edge.Second.GetPoint.Y);
            DrawVertex(edge.First);
            DrawVertex(edge.Second);
        }

        private void DrawLoop(Loop loop)
        {
            graphics.DrawArc(darkGoldPen, loop.First.GetPoint.X - 2 * R, loop.First.GetPoint.Y - 2 * R, 2 * R, 2 * R, 90, 270);
            DrawVertex(loop.First);
        }

        private void button2_Click(object sender, EventArgs e) // вершина
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e) // Ребро
        {
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void button4_Click(object sender, EventArgs e) // удалить элемент
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
            if (!button2.Enabled)
            {
                Vertex vertexToAdd = new Vertex(new Point(e.X, e.Y));
                fieldGraph.AddElement(vertexToAdd);
                DrawVertex(vertexToAdd);
                pictureBox1.Image = bitmap;
            } 
            else if (!button3.Enabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var el in Vertex.Vertices)
                    {
                        if (Math.Pow(el.GetPoint.X - e.X, 2) + Math.Pow(el.GetPoint.Y - e.Y, 2) <= R * R)
                        {
                            if (firstVertex == SelectedVertex.None)
                            {
                                graphics.DrawEllipse(redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);
                                _first = el;
                                firstVertex = SelectedVertex.First;
                                numberOfSelectedFirstVertex = GetNumberOfVertex(el) - 1;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                            if (secondVertex == SelectedVertex.None)
                            {
                                graphics.DrawEllipse(redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);
                                numberOfSelectedSecondVertex = GetNumberOfVertex(el) - 1;
                                _Second = el;
                                if (numberOfSelectedFirstVertex == numberOfSelectedSecondVertex)
                                {
                                    fieldGraph.AddElement(new Loop(el));
                                    DrawLoop(new Loop(el));
                                }
                                else
                                {
                                    fieldGraph.AddElement(new Edge(_first, _Second));
                                    DrawEdge(new Edge(_first, _Second));
                                }
                                firstVertex = SelectedVertex.None;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((firstVertex != SelectedVertex.None) &&
                        (Math.Pow(_first.GetPoint.X - e.X, 2) + Math.Pow(_Second.GetPoint.Y - e.Y, 2) <= R * R))
                    {
                        DrawVertex(_first);
                        firstVertex = SelectedVertex.None;
                        pictureBox1.Image = bitmap;
                    }
                }
            }
            else if (!button4.Enabled)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                foreach (var el in Vertex.Vertices)
                {
                    if (Math.Pow(el.GetPoint.X - e.X, 2) + Math.Pow(el.GetPoint.Y - e.Y, 2) <= R * R)
                    {
                        fieldGraph.RemoveElement(el);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    foreach (var el in Edge.Edges)
                    {
                        if (el is Loop) //если это петля
                        {
                            if ((Math.Pow(el.First.GetPoint.X - R - e.X, 2) + Math.Pow(el.First.GetPoint.Y - R - e.Y, 2) <= ((R + 2) * (R + 2))) &&
                                (Math.Pow(el.First.GetPoint.X - R - e.X, 2) + Math.Pow(el.First.GetPoint.Y - R - e.Y, 2) >= ((R - 2) * (R - 2))))
                            {
                                fieldGraph.RemoveElement(el);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - el.First.GetPoint.X) * (el.Second.GetPoint.Y - el.First.GetPoint.Y) / (el.Second.GetPoint.X - el.First.GetPoint.X) + el.First.GetPoint.Y) <= (e.Y + 4)
                                && ((e.X - el.First.GetPoint.X) * (el.Second.GetPoint.Y - el.First.GetPoint.Y) / (el.Second.GetPoint.X - el.First.GetPoint.X) + el.First.GetPoint.Y) >= (e.Y - 4))
                            {
                                if ((el.First.GetPoint.X <= el.Second.GetPoint.X && el.First.GetPoint.X <= e.X && e.X <= el.Second.GetPoint.X) ||
                                    (el.First.GetPoint.X >= el.Second.GetPoint.X && el.First.GetPoint.X >= e.X && e.X >= el.Second.GetPoint.X))
                                {
                                    fieldGraph.RemoveElement(el);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (flag)
                {
                    graphics.Clear(Color.White);
                    DrawAllGraph();
                    pictureBox1.Image = bitmap;
                }
            }
        }

        private void DrawAllGraph()
        {
            foreach (var edge in Edge.Edges)
            {
                if (edge is Loop loop)
                {
                    DrawLoop(loop);
                }
                else
                {
                    DrawEdge(edge);
                }
            }

            foreach (var vertex in Vertex.Vertices)
            {
                DrawVertex(vertex);
            }
        }
    }
}
