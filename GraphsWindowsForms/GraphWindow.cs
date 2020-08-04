using GraphModel.Assets.Model.GraphElements;
using GraphsLibrary.Assets.Model.GraphElements;
using GraphsLibrary.Assets.Model.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Point = GraphsLibrary.Assets.Model.Utils.Point;

namespace GraphsWindowForms
{
    public partial class GraphWindow : Form
    {
        private readonly List<string> _chainList = new List<string>();

        private readonly Bitmap _bitmap;
        private readonly Pen _blackPen;
        private readonly Pen _redPen;
        private readonly Pen _darkGoldPen;
        private readonly Graphics _graphics;
        private readonly Font _font;
        private readonly Brush _brush;
        private PointF _pointF;
        private const int R = 20;

        private readonly ElementContainer<Vertex> _vertices;
        private readonly ElementContainer<Edge> _edges;

        private AdjacencyMatrix Matrix
        {
            get
            {
                return new AdjacencyMatrix(_vertices, _edges);
            }
        }

        private Vertex _first;
        private Vertex _second;

        private int StartVertexNumber => Convert.ToInt32(StartVertexNumberTextBox.Text);

        private int TargetVertexNumber => Convert.ToInt32(TargetVertexNumberTextBox.Text);

        public GraphWindow()
        {
            InitializeComponent();

            _bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            _graphics = Graphics.FromImage(_bitmap);
            _graphics.Clear(Color.White);

            _blackPen = new Pen(Color.Black)
            {
                Width = 2
            };

            _redPen = new Pen(Color.Red)
            {
                Width = 2
            };

            _darkGoldPen = new Pen(Color.DarkGoldenrod)
            {
                Width = 2
            };

            _font = new Font("Arial", 15);
            _brush = Brushes.Black;

            _vertices = new ElementContainer<Vertex>();
            _edges = new ElementContainer<Edge>();

            pictureBox1.Image = _bitmap;
        }

        private void DrawVertex(Vertex vertex)
        {
            _graphics.FillEllipse(Brushes.White, (vertex.Point.X - R), (vertex.Point.Y - R), 2 * R, 2 * R);
            _graphics.DrawEllipse(_blackPen, (vertex.Point.X - R), (vertex.Point.Y - R), 2 * R, 2 * R);
            _pointF = new PointF(vertex.Point.X - 9, vertex.Point.Y - 9);
            _graphics.DrawString((_vertices.IndexOf(vertex) + 1).ToString(), _font, _brush, _pointF);
        }

        private void DrawEdge(Edge edge)
        {
            _graphics.DrawLine(_darkGoldPen, edge.Start.Point.X, edge.Start.Point.Y, edge.End.Point.X, edge.End.Point.Y);
            DrawVertex(edge.Start);
            DrawVertex(edge.End);
        }

        private void DrawLoop(Loop loop)
        {
            _graphics.DrawArc(_darkGoldPen, loop.Start.Point.X - 2 * R, loop.Start.Point.Y - 2 * R, 2 * R, 2 * R, 90, 270);
            DrawVertex(loop.Start);
        }

        private void DrawAllGraph()
        {
            foreach (var edge in _edges)
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

            foreach (var vertex in _vertices)
            {
                DrawVertex(vertex);
            }
        }

        private void GraphPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!DrawVertexButton.Enabled)
            {
                Vertex vertexToAdd = new Vertex(new Point(e.X, e.Y));
                _vertices.Add(vertexToAdd);
                DrawVertex(vertexToAdd);
                pictureBox1.Image = _bitmap;
            }
            else if (!DrawEdgeButton.Enabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var el in _vertices)
                    {
                        if (Math.Pow(el.Point.X - e.X, 2) + Math.Pow(el.Point.Y - e.Y, 2) <= R * R)
                        {
                            if (_first == null)
                            {
                                _first = el;
                                _graphics.DrawEllipse(_redPen, el.Point.X - R, el.Point.Y - R, 2 * R, 2 * R);
                                pictureBox1.Image = _bitmap;
                                break;
                            }
                            if (_second == null)
                            {
                                _second = el;
                                _graphics.DrawEllipse(_redPen, el.Point.X - R, el.Point.Y - R, 2 * R, 2 * R);

                                if (_first == _second)
                                {
                                    _edges.Add(new Loop(el));
                                    DrawLoop(new Loop(el));
                                }
                                else
                                {
                                    _edges.Add(new Edge(_first, _second));
                                    DrawEdge(new Edge(_first, _second));
                                }

                                _first = null;
                                _second = null;
                                pictureBox1.Image = _bitmap;
                                break;
                            }
                        }
                    }
                }
            }
            else if (!DeleteElementButton.Enabled)
            {
                bool flag = false;

                foreach (var el in _vertices)
                {
                    if (Math.Pow(el.Point.X - e.X, 2) + Math.Pow(el.Point.Y - e.Y, 2) <= R * R)
                    {
                        _vertices.Remove(el);
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    foreach (var el in _edges)
                    {
                        if (el is Loop)
                        {
                            if (Math.Pow(el.Start.Point.X - R - e.X, 2) + Math.Pow(el.Start.Point.Y - R - e.Y, 2) <= ((R + 2) * (R + 2)))
                            {
                                _edges.Remove(el);
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            float regionOfClick = (e.X - el.Start.Point.X) * (el.End.Point.Y - el.Start.Point.Y) / (el.End.Point.X - el.Start.Point.X) + el.Start.Point.Y;

                            if (regionOfClick <= (e.Y + 4) && regionOfClick >= (e.Y - 4))
                            {
                                _edges.Remove(el);
                                flag = true;
                                break;
                            }
                        }
                    }
                }

                if (flag)
                {
                    _graphics.Clear(Color.White);
                    DrawAllGraph();
                    pictureBox1.Image = _bitmap;
                }
            }
        }

        private void DrawVertexButton_Click(object sender, EventArgs e)
        {
            DrawVertexButton.Enabled = false;
            DrawEdgeButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            _graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = _bitmap;
        }

        private void DrawEdgeButton_Click(object sender, EventArgs e)
        {
            DrawEdgeButton.Enabled = false;
            DrawVertexButton.Enabled = true;
            DeleteElementButton.Enabled = true;
            _graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = _bitmap;
        }

        private void DeleteElementButton_Click(object sender, EventArgs e)
        {
            DeleteElementButton.Enabled = false;
            DrawVertexButton.Enabled = true;
            DrawEdgeButton.Enabled = true;
            _graphics.Clear(Color.White);
            DrawAllGraph();
            pictureBox1.Image = _bitmap;
        }

        private void DFSchain(int u, int numberEndVertex, int[] color, string s)
        {
            if (u != numberEndVertex)
            {
                color[u] = 2;
            }
            else
            {
                _chainList.Add(s);
                ListAllWaysListBox.Items.Add(s);
                return;
            }
            foreach (var el in _edges)
            {
                if (color[_vertices.IndexOf(el.End)] == 1 && _vertices.IndexOf(el.Start) == u)
                {
                    AddChain(numberEndVertex, color, s, _vertices.IndexOf(el.End));
                }
                else if (color[_vertices.IndexOf(el.Start)] == 1 && _vertices.IndexOf(el.End) == u)
                {
                    AddChain(numberEndVertex, color, s, _vertices.IndexOf(el.Start));
                }
            }
        }

        private void AddChain(int endV, int[] color, string s, int numberVertex)
        {
            DFSchain(numberVertex, endV, color, s + (numberVertex + 1).ToString());
            color[numberVertex] = 1;
        }

        private void SearchAllWaysButton_Click(object sender, EventArgs e)
        {
            _chainList.Clear();

            int[] color = new int[_vertices.Count];
            for (int i = 0; i < _vertices.Count - 1; i++)
            {
                for (int j = i + 1; j < _vertices.Count; j++)
                {
                    for (int k = 0; k < _vertices.Count; k++)
                    {
                        color[k] = 1;
                    }

                    DFSchain(i, j, color, (i + 1).ToString());
                }
            }

            List<string> newChainList = new List<string>();
            foreach (string i in _chainList)
            {
                if (i.StartsWith(StartVertexNumberTextBox.Text) && i.EndsWith(TargetVertexNumberTextBox.Text))
                {
                    newChainList.Add(i);
                }
                else if (i.EndsWith(StartVertexNumberTextBox.Text) && i.StartsWith(TargetVertexNumberTextBox.Text))
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
            MessageBox.Show("Кратчайший путь от вершины " + StartVertexNumber + " до вершины " + TargetVertexNumber +
                            " равен: " + Matrix.SearchShortestWay(StartVertexNumber, TargetVertexNumber) + ".");
        }
    }
}
