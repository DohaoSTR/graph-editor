using GraphsLibrary.GraphElements;
using GraphsLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Point = GraphsLibrary.GraphElements.Point;

namespace GraphsWindowForms
{
    public partial class GraphWindow : Form
    {
        private readonly Bitmap _bitmap;
        private readonly Pen _blackPen;
        private readonly Pen _redPen;
        private readonly Pen _darkGoldPen;
        private readonly Graphics _graphics;
        private readonly Font _font;
        private readonly Brush _brush;
        private PointF _pointF;
        private const int R = 20;

        private readonly Graph _graph;

        private Vertex _first;
        private Vertex _second;

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

            _graph = new Graph();

            pictureBox1.Image = _bitmap;
        }

        private AdjacencyMatrix Matrix
        {
            get
            {
                return new AdjacencyMatrix((ICollection<Vertex>)_graph.Vertices, (ICollection<Edge>)_graph.Edges);
            }
        }

        private int StartVertexNumber => Convert.ToInt32(StartVertexNumberTextBox.Text);

        private int TargetVertexNumber => Convert.ToInt32(TargetVertexNumberTextBox.Text);

        private void DrawVertex(Vertex vertex)
        {
            _graphics.FillEllipse(Brushes.White, (vertex.Point.X - R), (vertex.Point.Y - R), 2 * R, 2 * R);
            _graphics.DrawEllipse(_blackPen, (vertex.Point.X - R), (vertex.Point.Y - R), 2 * R, 2 * R);
            _pointF = new PointF(vertex.Point.X - 9, vertex.Point.Y - 9);
            _graphics.DrawString((_graph.Vertices.IndexOf(vertex) + 1).ToString(), _font, _brush, _pointF);
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
            foreach (var edge in _graph.Edges)
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

            foreach (var vertex in _graph.Vertices)
            {
                DrawVertex(vertex);
            }
        }

        private void GraphPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!DrawVertexButton.Enabled)
            {
                Vertex vertexToAdd = new Vertex(new Point(e.X, e.Y));
                _graph.Vertices.Add(vertexToAdd);
                DrawVertex(vertexToAdd);
                pictureBox1.Image = _bitmap;
            }
            else if (!DrawEdgeButton.Enabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var el in _graph.Vertices)
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
                                    _graph.Edges.Add(new Loop(el));
                                    DrawLoop(new Loop(el));
                                }
                                else
                                {
                                    _graph.Edges.Add(new Edge(_first, _second));
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

                foreach (var el in _graph.Vertices)
                {
                    if (Math.Pow(el.Point.X - e.X, 2) + Math.Pow(el.Point.Y - e.Y, 2) <= R * R)
                    {
                        _graph.Vertices.Remove(el);
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    foreach (var el in _graph.Edges)
                    {
                        if (el is Loop)
                        {
                            if (Math.Pow(el.Start.Point.X - R - e.X, 2) + Math.Pow(el.Start.Point.Y - R - e.Y, 2) <= ((R + 2) * (R + 2)))
                            {
                                _graph.Edges.Remove(el);
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            float regionOfClick = (e.X - el.Start.Point.X) * (el.End.Point.Y - el.Start.Point.Y) / (el.End.Point.X - el.Start.Point.X) + el.Start.Point.Y;

                            if (regionOfClick <= (e.Y + 4) && regionOfClick >= (e.Y - 4))
                            {
                                _graph.Edges.Remove(el);
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

        private void SearchAllWaysButton_Click(object sender, EventArgs e)
        {
            ListAllWaysListBox.Items.Clear();
            DFsUtil util = new DFsUtil();
            foreach (string i in util.GetChains(_graph.Vertices, _graph.Edges, Convert.ToInt32(StartVertexNumberTextBox.Text), Convert.ToInt32(TargetVertexNumberTextBox.Text)))
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
