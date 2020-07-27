using GraphModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Wave_Algorithm
{
    public partial class Form1 : Form
    {
        private static List<string> chainList = new List<string>();       

        private Bitmap bitmap;
        private Pen blackPen;
        private Pen redPen;
        private Pen darkGoldPen;
        private Graphics graphics;
        private Font font;
        private Brush brush;
        private PointF point;
        private int R = 20; //радиус окружности вершины

        private FieldGraph fieldGraph;

        private Vertex selected1; //выбранные вершины, для соединения линиями
        private Vertex selected2;


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

        private void DrawVertex(int x, int y, string number)
        {
            graphics.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            graphics.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            graphics.DrawString(number, fo, br, point);
        }

        private void DrawEdge(Vertex V1, Vertex V2, Edge E)
        {
            if (E.v1 == E.v2)
            {
                graphics.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                graphics.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                DrawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }

        private void DrawVertexButton2_Click(object sender, EventArgs e) //кнопка - рисовать вершину
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            graphics.Clear(Color.White);
            DrawALLGraph(verticies, E);
            pictureBox1.Image = bitmap;
        }
        private void DrawEdgeButton3_Click(object sender, EventArgs e) //кнопка - рисовать ребро
        {
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            gr.Clear(Color.White);
            DrawALLGraph(verticies, E);
            pictureBox1.Image = bitmap;
            selected1 = -1;
            selected2 = -1;
        }

        private void DeleteButton4_Click(object sender, EventArgs e) //кнопка - удалить элемент
        {
            button4.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            gr.Clear(Color.White);
            DrawALLGraph(verticies, E);
            pictureBox1.Image = bitmap;
        }
        private void DeleteALLButton_Click(object sender, EventArgs e) //кнопка - удалить граф
        {
            const string message = "Вы действительно хотите полностью очистить граф?";
            const string caption = "Очистка";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                verticies.Clear();
                E.Clear();
                gr.Clear(Color.White);
                pictureBox1.Image = bitmap;
            }
        }
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e) // проверяем какая кнопка нажата ( рисовать вершину ребро и т.д)
        {
            //нажата кнопка "рисовать вершину"
            if (button2.Enabled == false)
            {
                verticies.Add(new Vertex(new GraphModel.Point(e.X, e.Y)));
                DrawVertex(e.X, e.Y, verticies.Count.ToString());
                pictureBox1.Image = bitmap;
            }
            //нажата кнопка "рисовать ребро"
            if (button3.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < verticies.Count; i++)
                    {
                        if (Math.Pow((verticies[i].x - e.X), 2) + Math.Pow((verticies[i].y - e.Y), 2) <= R * R)
                        {
                            if (selected1 == -1)
                            {
                                gr.DrawEllipse(redPen, (verticies[i].x - R), (verticies[i].y - R), 2 * R, 2 * R);
                                selected1 = i;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                            if (selected2 == -1)
                            {
                                gr.DrawEllipse(redPen, (verticies[i].x - R), (verticies[i].y - R), 2 * R, 2 * R);
                                selected2 = i;
                                E.Add(new Edge(selected1, selected2));
                                DrawEdge(verticies[selected1], verticies[selected2], E[E.Count - 1]);
                                selected1 = -1;
                                selected2 = -1;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((verticies[selected1].x - e.X), 2) + Math.Pow((verticies[selected1].y - e.Y), 2) <= R * R))
                    {
                        DrawVertex(verticies[selected1].x, verticies[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        pictureBox1.Image = bitmap;
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (button4.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < verticies.Count; i++)
                {
                    if (Math.Pow((verticies[i].x - e.X), 2) + Math.Pow((verticies[i].y - e.Y), 2) <= R * R)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if ((E[j].v1 == i) || (E[j].v2 == i))
                            {
                                E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (E[j].v1 > i) E[j].v1--;
                                if (E[j].v2 > i) E[j].v2--;
                            }
                        }
                        verticies.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    for (int i = 0; i < E.Count; i++)
                    {
                        if (E[i].v1 == E[i].v2) //если это петля
                        {
                            if ((Math.Pow(verticies[E[i].v1].x - R - e.X, 2) + Math.Pow((verticies[E[i].v1].y - R - e.Y), 2) <= ((R + 2) * (R + 2))) &&
                                (Math.Pow((verticies[E[i].v1].x - R - e.X), 2) + Math.Pow((verticies[E[i].v1].y - R - e.Y), 2) >= ((R - 2) * (R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - verticies[E[i].v1].x) * (verticies[E[i].v2].y - verticies[E[i].v1].y) / (verticies[E[i].v2].x - verticies[E[i].v1].x) + verticies[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - verticies[E[i].v1].x) * (verticies[E[i].v2].y - verticies[E[i].v1].y) / (verticies[E[i].v2].x - verticies[E[i].v1].x) + verticies[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((verticies[E[i].v1].x <= verticies[E[i].v2].x && verticies[E[i].v1].x <= e.X && e.X <= verticies[E[i].v2].x) ||
                                    (verticies[E[i].v1].x >= verticies[E[i].v2].x && verticies[E[i].v1].x >= e.X && e.X >= verticies[E[i].v2].x))
                                {
                                    E.RemoveAt(i);
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
                    gr.Clear(Color.White);
                    DrawALLGraph(verticies, E);
                    pictureBox1.Image = bitmap;
                }
            }
        }

        private void DrawALLGraph(List<Vertex> V, List<Edge> E) // Метод сохраняющий вершины на pictureBox
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                DrawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }
        private void FillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }
        private void AlgorithmLee(object sender, EventArgs e) // Волновой алгоритм
        {
            int[,] AMatrix = new int[verticies.Count, verticies.Count];
            int k, j;
            FillAdjacencyMatrix(verticies.Count, E, AMatrix);

            int start = Convert.ToInt32(textBox1.Text) - 1;
            int target = Convert.ToInt32(textBox2.Text) - 1;
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
        private void DFSchain(int u, int endV, List<Edge> E, int[] color, string s) // алгоритм вывода всех путей из старта в финиш
        {
            if (u != endV)
                color[u] = 2;
            else
            {
                chainList.Add(s);
                listBox1.Items.Add(s);
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    DFSchain(E[w].v2, endV, E, color, s + (E[w].v2 + 1).ToString());
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    DFSchain(E[w].v1, endV, E, color, s + (E[w].v1 + 1).ToString());
                    color[E[w].v1] = 1;
                }
            }
        }
        private void СhainButton_Click(object sender, EventArgs e) // вывод всех путей из старта в финиш (листбокс)
        {
            chainList.Clear();
            int[] color = new int[verticies.Count];
            for (int i = 0; i < verticies.Count - 1; i++)
                for (int j = i + 1; j < verticies.Count; j++)
                {
                    for (int k = 0; k < verticies.Count; k++)
                        color[k] = 1;
                    DFSchain(i, j, E, color, (i + 1).ToString());
                }
            List<string> newChainList = new List<string>();
           
            foreach (string i in chainList)
            {
                if (i.StartsWith(textBox1.Text) && i.EndsWith(textBox2.Text))
                    newChainList.Add(i);
                if (i.EndsWith(textBox1.Text) && i.StartsWith(textBox2.Text))
                {
                    char[] arr = i.ToCharArray();
                    Array.Reverse(arr);
                    string j = new string(arr);
                    newChainList.Add(j);
                }
            }
            listBox1.Items.Clear();
            foreach (string i in newChainList)
            {
                listBox1.Items.Add(i);
            }
        }
    }
}
