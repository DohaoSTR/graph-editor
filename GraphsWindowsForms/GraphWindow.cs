using GraphModel.Assets.Model.GraphElements;
using GraphsLibrary.Assets.Model.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Point = GraphsLibrary.Assets.Model.Utils.Point;

namespace Wave_Algorithm
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

        private readonly FieldGraph _fieldGraph;

        private AdjacencyMatrix Matrix
        {
            get
            {
                return new AdjacencyMatrix((List<Vertex>)Vertex.Vertices, (List<Edge>)Edge.Edges);
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

            _fieldGraph = new FieldGraph();

            pictureBox1.Image = _bitmap;
        }

        private void DrawVertex(Vertex vertex)
        {
            _graphics.FillEllipse(Brushes.White, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            _graphics.DrawEllipse(_blackPen, (vertex.GetPoint.X - R), (vertex.GetPoint.Y - R), 2 * R, 2 * R);
            _pointF = new PointF(vertex.GetPoint.X - 9, vertex.GetPoint.Y - 9);
            _graphics.DrawString((vertex.GetNumber + 1).ToString(), _font, _brush, _pointF);
        }

        private void DrawEdge(Edge edge)
        {
            _graphics.DrawLine(_darkGoldPen, edge.Start.GetPoint.X, edge.Start.GetPoint.Y, edge.End.GetPoint.X, edge.End.GetPoint.Y);
            DrawVertex(edge.Start);
            DrawVertex(edge.End);
        }

        private void DrawLoop(Loop loop)
        {
            _graphics.DrawArc(_darkGoldPen, loop.Start.GetPoint.X - 2 * R, loop.Start.GetPoint.Y - 2 * R, 2 * R, 2 * R, 90, 270);
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
                _fieldGraph.AddElement(vertexToAdd);
                DrawVertex(vertexToAdd);
                pictureBox1.Image = _bitmap;
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
                                _graphics.DrawEllipse(_redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);
                                pictureBox1.Image = _bitmap;
                                break;
                            }
                            if (_second == null)
                            {
                                _second = el;
                                _graphics.DrawEllipse(_redPen, el.GetPoint.X - R, el.GetPoint.Y - R, 2 * R, 2 * R);

                                if (_first == _second)
                                {
                                    _fieldGraph.AddElement(new Loop(el));
                                    DrawLoop(new Loop(el));
                                }
                                else
                                {
                                    _fieldGraph.AddElement(new Edge(_first, _second));
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

                foreach (var el in Vertex.Vertices)
                {
                    if (Math.Pow(el.GetPoint.X - e.X, 2) + Math.Pow(el.GetPoint.Y - e.Y, 2) <= R * R)
                    {
                        _fieldGraph.RemoveElement(el);
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
                            if (Math.Pow(el.Start.GetPoint.X - R - e.X, 2) + Math.Pow(el.Start.GetPoint.Y - R - e.Y, 2) <= ((R + 2) * (R + 2)))
                            {
                                _fieldGraph.RemoveElement(el);
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            float regionOfClick = (e.X - el.Start.GetPoint.X) * (el.End.GetPoint.Y - el.Start.GetPoint.Y) / (el.End.GetPoint.X - el.Start.GetPoint.X) + el.Start.GetPoint.Y;

                            if (regionOfClick <= (e.Y + 4) && regionOfClick >= (e.Y - 4))
                            {
                                _fieldGraph.RemoveElement(el);
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
            foreach (var el in Edge.Edges)
            {
                if (color[el.End.GetNumber] == 1 && el.Start.GetNumber == u)
                {
                    AddChain(numberEndVertex, color, s, el.End.GetNumber);
                }
                else if (color[el.Start.GetNumber] == 1 && el.End.GetNumber == u)
                {
                    AddChain(numberEndVertex, color, s, el.Start.GetNumber);
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

            int[] color = new int[Vertex.Vertices.Count];
            for (int i = 0; i < Vertex.Vertices.Count - 1; i++)
            {
                for (int j = i + 1; j < Vertex.Vertices.Count; j++)
                {
                    for (int k = 0; k < Vertex.Vertices.Count; k++)
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
