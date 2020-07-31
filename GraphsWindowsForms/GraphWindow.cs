using GraphModel.Assets.Model;
using GraphModel.Assets.Model.GraphElements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Point = GraphModel.Assets.Model.Point;

namespace Wave_Algorithm
{
    public partial class GraphWindow : Form
    {
        private readonly List<string> _chainList = new List<string>();

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
        private Vertex _second;

        public GraphWindow()
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
            graphics.FillEllipse(Brushes.White, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            pointf = new PointF(vertex.GetPoint.X - 9, vertex.GetPoint.Y - 9);
            graphics.DrawString((vertex.GetNumber + 1).ToString(), font, brush, pointf);
        }

        private void DrawEdge(Edge edge)
        {
            graphics.DrawLine(darkGoldPen, edge.Start.GetPoint.X, edge.Start.GetPoint.Y, edge.End.GetPoint.X, edge.End.GetPoint.Y);
            DrawVertex(edge.Start);
            DrawVertex(edge.End);
        }

        private void DrawLoop(Loop loop)
        {
            graphics.DrawArc(darkGoldPen, loop.Start.GetPoint.X - 2 * R, loop.Start.GetPoint.Y - 2 * R, 2 * R, 2 * R, 90, 270);
            DrawVertex(loop.Start);
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

        private void GraphPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!DrawVertexButton.Enabled)
            {
                Vertex vertexToAdd = new Vertex(new Point(e.X, e.Y));
                fieldGraph.AddElement(vertexToAdd);
                DrawVertex(vertexToAdd);
                pictureBox1.Image = bitmap;
            } 
            else if (!DrawEdgeButton.Enabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var el in Vertex.Vertices)
                    {
                        if (Math.Pow(el.GetPoint.X - e.X, 2) + Math.Pow(el.GetPoint.Y - e.Y, 2) <= R * R)
                        {
                            if (_first == null)
                            {
                                _first = el;
                                graphics.DrawEllipse(redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);
                                pictureBox1.Image = bitmap;
                                break;
                            }
                            if (_second == null)
                            {
                                _second = el;
                                graphics.DrawEllipse(redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);

                                if (_first == _second)
                                {
                                    fieldGraph.AddElement(new Loop(el));
                                    DrawLoop(new Loop(el));
                                }
                                else
                                {
                                    fieldGraph.AddElement(new Edge(_first, _second));
                                    DrawEdge(new Edge(_first, _second));
                                }

                                _first = null;
                                _second = null;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((_first != null) && (Math.Pow(_first.GetPoint.X - e.X, 2) + Math.Pow(_second.GetPoint.Y - e.Y, 2) <= R * R))
                    {
                        DrawVertex(_first);
                        pictureBox1.Image = bitmap;
                    }
                }
            }
            else if (!DeleteElementButton.Enabled)
            {
                bool flag = false;

                foreach (var el in Vertex.Vertices)
                {
                    if (Math.Pow(el.GetPoint.X - e.X, 2) + Math.Pow(el.GetPoint.Y - e.Y, 2) <= R * R)
                    {
                        fieldGraph.RemoveElement(el);
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    foreach (var el in Edge.Edges)
                    {
                        if (el is Loop) 
                        {
                            if ((Math.Pow(el.Start.GetPoint.X - R - e.X, 2) + Math.Pow(el.Start.GetPoint.Y - R - e.Y, 2) <= ((R + 2) * (R + 2))) &&
                                (Math.Pow(el.Start.GetPoint.X - R - e.X, 2) + Math.Pow(el.Start.GetPoint.Y - R - e.Y, 2) >= ((R - 2) * (R - 2))))
                            {
                                fieldGraph.RemoveElement(el);
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            if (((e.X - el.Start.GetPoint.X) * (el.End.GetPoint.Y - el.Start.GetPoint.Y) / (el.End.GetPoint.X - el.Start.GetPoint.X) + el.Start.GetPoint.Y) <= (e.Y + 4)
                                && ((e.X - el.Start.GetPoint.X) * (el.End.GetPoint.Y - el.Start.GetPoint.Y) / (el.End.GetPoint.X - el.Start.GetPoint.X) + el.Start.GetPoint.Y) >= (e.Y - 4))
                            {
                                if ((el.Start.GetPoint.X <= el.End.GetPoint.X && el.Start.GetPoint.X <= e.X && e.X <= el.End.GetPoint.X) ||
                                    (el.Start.GetPoint.X >= el.End.GetPoint.X && el.Start.GetPoint.X >= e.X && e.X >= el.End.GetPoint.X))
                                {
                                    fieldGraph.RemoveElement(el);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (flag)
                {
                    graphics.Clear(Color.White);
                    DrawAllGraph();
                    pictureBox1.Image = bitmap;
                }
            }
        }

        private void DrawVertexButton_Click(object sender, EventArgs e)
        {
            DrawVertexButton.Enabled = false;
            DrawEdgeButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void DrawEdgeButton_Click(object sender, EventArgs e)
        {
            DrawEdgeButton.Enabled = false;
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private void DeleteElementButton_Click(object sender, EventArgs e)
        {
            DeleteElementButton.Enabled = false;
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = bitmap;
        }

        private int[,] FillAdjacencyMatrix()
        {
            int[,] matrix = new int[Vertex.Vertices.Count, Vertex.Vertices.Count];
            for (int i = 0; i < Vertex.Vertices.Count; i++)
                for (int j = 0; j < Vertex.Vertices.Count; j++)
                    matrix[i, j] = 0;
            foreach(var el in Edge.Edges)
            {
                matrix[el.Start.GetNumber, el.End.GetNumber] = 1;
                matrix[el.End.GetNumber, el.Start.GetNumber] = 1;
            }
            return matrix;
        }
        
        private void DFSchain(int u, int endV, int[] color, string s)
        {
            if (u != endV)
                color[u] = 2;
            else
            {
                _chainList.Add(s);
                ListAllWaysListBox.Items.Add(s);
                return;
            }
            foreach (var el in Edge.Edges)
            {
                if (color[el.End.GetNumber] == 1 && el.Start.GetNumber == u)
                {
                    DFSchain(el.End.GetNumber, endV, color, s + (el.End.GetNumber + 1).ToString());
                    color[el.End.GetNumber] = 1;
                }
                else if (color[el.Start.GetNumber] == 1 && el.End.GetNumber == u)
                {
                    DFSchain(el.Start.GetNumber, endV, color, s + (el.Start.GetNumber + 1).ToString());
                    color[el.Start.GetNumber] = 1;
                }
            }
        }

        private void SearchAllWaysButton_Click(object sender, EventArgs e)
        {
            _chainList.Clear();
            int[] color = new int[Vertex.Vertices.Count];
            for (int i = 0; i < Vertex.Vertices.Count - 1; i++)
                for (int j = i + 1; j < Vertex.Vertices.Count; j++)
                {
                    for (int k = 0; k < Vertex.Vertices.Count; k++)
                        color[k] = 1;
                    DFSchain(i, j, color, (i + 1).ToString());
                }
            List<string> newChainList = new List<string>();

            foreach (string i in _chainList)
            {
                if (i.StartsWith(StartVertexNumberTextBox.Text) && i.EndsWith(TargetVertexNumberTextBox.Text))
                    newChainList.Add(i);
                if (i.EndsWith(StartVertexNumberTextBox.Text) && i.StartsWith(TargetVertexNumberTextBox.Text))
                {
                    char[] arr = i.ToCharArray();
                    Array.Reverse(arr);
                    string j = new string(arr);
                    newChainList.Add(j);
                }
            }
            ListAllWaysListBox.Items.Clear();
            foreach (string i in newChainList)
            {
                ListAllWaysListBox.Items.Add(i);
            }
        }

        private void FindShortWayButton_Click(object sender, EventArgs e)
        {
            int[,] AMatrix = FillAdjacencyMatrix();
            int k, j;

            int start = Convert.ToInt32(StartVertexNumberTextBox.Text) - 1;
            int target = Convert.ToInt32(TargetVertexNumberTextBox.Text) - 1;
            int[] p = new int[AMatrix.GetLength(0)];

            for (int i = 0; i < AMatrix.GetLength(0); i++)
                p[i] = -1;

            p[start] = 0;
            for (int i = 0; i < AMatrix.GetLength(0); i++)
                for (k = 0; k < AMatrix.GetLength(0); k++)
                    if (p[k] == i)
                        for (j = 0; j < AMatrix.GetLength(0); j++)
                        {
                            if (p[j] == -1 && AMatrix[j, k] == 1)
                                p[j] = i + 1;
                        }
            for (int i = 0; i < AMatrix.GetLength(0); i++)
            {
                if (i == target)
                {
                    MessageBox.Show("Кратчайший путь от вершины " + (start + 1) + " до вершины " + (target + 1) + " равен: " + p[i] + ".");
                }
            }
        }
    }
}
