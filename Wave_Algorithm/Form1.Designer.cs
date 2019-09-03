namespace Wave_Algorithm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 426);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "Построить вершину";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DrawVertexButton2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 81);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "Построить ребро";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DrawEdgeButton3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 124);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 37);
            this.button4.TabIndex = 4;
            this.button4.Text = "Удалить элемент";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.DeleteButton4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйГрафToolStripMenuItem,
            this.сохранитьГрафToolStripMenuItem,
            this.загрузитьГрафToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйГрафToolStripMenuItem
            // 
            this.новыйГрафToolStripMenuItem.Name = "новыйГрафToolStripMenuItem";
            this.новыйГрафToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.новыйГрафToolStripMenuItem.Text = "Очистить граф";
            this.новыйГрафToolStripMenuItem.Click += new System.EventHandler(this.DeleteALLButton_Click);
            // 
            // сохранитьГрафToolStripMenuItem
            // 
            this.сохранитьГрафToolStripMenuItem.Name = "сохранитьГрафToolStripMenuItem";
            this.сохранитьГрафToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.сохранитьГрафToolStripMenuItem.Text = "Сохранить граф";
            this.сохранитьГрафToolStripMenuItem.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // загрузитьГрафToolStripMenuItem
            // 
            this.загрузитьГрафToolStripMenuItem.Name = "загрузитьГрафToolStripMenuItem";
            this.загрузитьГрафToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.загрузитьГрафToolStripMenuItem.Text = "Загрузить граф";
            this.загрузитьГрафToolStripMenuItem.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(567, 415);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(194, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Найти кратчайший путь";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.AlgorithmLee);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(673, 389);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(88, 20);
            this.textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(567, 389);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 20);
            this.textBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Вершина старта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(670, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Вершина финиша";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Найти все пути";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.СhainButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(567, 55);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(221, 212);
            this.listBox1.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(564, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Старт - финиш (все пути)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Algorithm Lee";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьГрафToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem загрузитьГрафToolStripMenuItem;
    }
}

