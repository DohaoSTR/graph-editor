using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Wave_Algorithm
{
    public partial class Form1 : Form
    {
        private static List<string> chainList = new List<string>();       

        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20; //радиус окружности вершины

        List<Vertex> V;
        List<Edge> E;

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
            V = new List<Vertex>();
            E = new List<Edge>();
            pictureBox1.Image = bitmap;
        }
        private void DrawVertexButton2_Click(object sender, EventArgs e) //кнопка - рисовать вершину
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            gr.Clear(Color.White);
            DrawALLGraph(V, E);
            pictureBox1.Image = bitmap;
        }
        private void DrawEdgeButton3_Click(object sender, EventArgs e) //кнопка - рисовать ребро
        {
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            gr.Clear(Color.White);
            DrawALLGraph(V, E);
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
            DrawALLGraph(V, E);
            pictureBox1.Image = bitmap;
        }
        private void DeleteALLButton_Click(object sender, EventArgs e) //кнопка - удалить граф
        {
            const string message = "Вы действительно хотите полностью очистить граф?";
            const string caption = "Очистка";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                V.Clear();
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
                V.Add(new Vertex(e.X, e.Y));
                DrawVertex(e.X, e.Y, V.Count.ToString());
                pictureBox1.Image = bitmap;
            }
            //нажата кнопка "рисовать ребро"
            if (button3.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= R * R)
                        {
                            if (selected1 == -1)
                            {
                                gr.DrawEllipse(redPen, (V[i].x - R), (V[i].y - R), 2 * R, 2 * R);
                                selected1 = i;
                                pictureBox1.Image = bitmap;
                                break;
                            }
                            if (selected2 == -1)
                            {
                                gr.DrawEllipse(redPen, (V[i].x - R), (V[i].y - R), 2 * R, 2 * R);
                                selected2 = i;
                                E.Add(new Edge(selected1, selected2));
                                DrawEdge(V[selected1], V[selected2], E[E.Count - 1]);
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
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= R * R))
                    {
                        DrawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
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
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= R * R)
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
                        V.RemoveAt(i);
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
                            if ((Math.Pow((V[E[i].v1].x - R - e.X), 2) + Math.Pow((V[E[i].v1].y - R - e.Y), 2) <= ((R + 2) * (R + 2))) &&
                                (Math.Pow((V[E[i].v1].x - R - e.X), 2) + Math.Pow((V[E[i].v1].y - R - e.Y), 2) >= ((R - 2) * (R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                    (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
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
                    DrawALLGraph(V, E);
                    pictureBox1.Image = bitmap;
                }
            }
        }
        private void SaveButton_Click(object sender, EventArgs e) //кнопка - сохранить граф
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void LoadButton_Click(object sender, EventArgs e) //кнопка - загрузить граф
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    bitmap = new Bitmap(open_dialog.FileName);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 
                    this.pictureBox1.Size = bitmap.Size;
                    pictureBox1.Image = bitmap;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DrawVertex(int x, int y, string number) // метод рисующий вершину
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }
        private void DrawEdge(Vertex V1, Vertex V2, Edge E) // метод рисующий ребро
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                DrawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                DrawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
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
            int[,] AMatrix = new int[V.Count, V.Count];
            int k, j;
            FillAdjacencyMatrix(V.Count, E, AMatrix);

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
            int[] color = new int[V.Count];
            for (int i = 0; i < V.Count - 1; i++)
                for (int j = i + 1; j < V.Count; j++)
                {
                    for (int k = 0; k < V.Count; k++)
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
    // класс описывающий вершину графа
    class Vertex
    {
        public int x, y;
        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }  
    // класс описывающий ребро графа
    class Edge
    {
        public int v1, v2;
        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
