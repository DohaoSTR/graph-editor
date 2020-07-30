namespace Wave_Algorithm
{
    partial class GraphWindow
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
            this.DrawVertexButton = new System.Windows.Forms.Button();
            this.DrawEdgeButton = new System.Windows.Forms.Button();
            this.DeleteElementButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearAllGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindShortWayButton = new System.Windows.Forms.Button();
            this.TargetVertexNumberTextBox = new System.Windows.Forms.TextBox();
            this.StartVertexNumberTextBox = new System.Windows.Forms.TextBox();
            this.StartVertexLabel = new System.Windows.Forms.Label();
            this.TargetVertexLabel = new System.Windows.Forms.Label();
            this.SearchAllWaysButton = new System.Windows.Forms.Button();
            this.ListAllWaysListBox = new System.Windows.Forms.ListBox();
            this.ListAllWaysLabel = new System.Windows.Forms.Label();
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
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphPictureBox_MouseClick);
            // 
            // DrawVertexButton
            // 
            this.DrawVertexButton.Location = new System.Drawing.Point(12, 38);
            this.DrawVertexButton.Name = "DrawVertexButton";
            this.DrawVertexButton.Size = new System.Drawing.Size(117, 37);
            this.DrawVertexButton.TabIndex = 2;
            this.DrawVertexButton.Text = "Построить вершину";
            this.DrawVertexButton.UseVisualStyleBackColor = true;
            this.DrawVertexButton.Click += new System.EventHandler(this.DrawVertexButton_Click);
            // 
            // DrawEdgeButton
            // 
            this.DrawEdgeButton.Location = new System.Drawing.Point(12, 81);
            this.DrawEdgeButton.Name = "DrawEdgeButton";
            this.DrawEdgeButton.Size = new System.Drawing.Size(117, 37);
            this.DrawEdgeButton.TabIndex = 3;
            this.DrawEdgeButton.Text = "Построить ребро";
            this.DrawEdgeButton.UseVisualStyleBackColor = true;
            this.DrawEdgeButton.Click += new System.EventHandler(this.DrawEdgeButton_Click);
            // 
            // DeleteElementButton
            // 
            this.DeleteElementButton.Location = new System.Drawing.Point(12, 124);
            this.DeleteElementButton.Name = "DeleteElementButton";
            this.DeleteElementButton.Size = new System.Drawing.Size(117, 37);
            this.DeleteElementButton.TabIndex = 4;
            this.DeleteElementButton.Text = "Удалить элемент";
            this.DeleteElementButton.UseVisualStyleBackColor = true;
            this.DeleteElementButton.Click += new System.EventHandler(this.DeleteElementButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearAllGraphToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // ClearAllGraphToolStripMenuItem
            // 
            this.ClearAllGraphToolStripMenuItem.Name = "ClearAllGraphToolStripMenuItem";
            this.ClearAllGraphToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ClearAllGraphToolStripMenuItem.Text = "Очистить граф";
            // 
            // FindShortWayButton
            // 
            this.FindShortWayButton.Location = new System.Drawing.Point(567, 415);
            this.FindShortWayButton.Name = "FindShortWayButton";
            this.FindShortWayButton.Size = new System.Drawing.Size(194, 23);
            this.FindShortWayButton.TabIndex = 8;
            this.FindShortWayButton.Text = "Найти кратчайший путь";
            this.FindShortWayButton.UseVisualStyleBackColor = true;
            this.FindShortWayButton.Click += new System.EventHandler(this.FindShortWayButton_Click);
            // 
            // TargetVertexNumberTextBox
            // 
            this.TargetVertexNumberTextBox.Location = new System.Drawing.Point(673, 389);
            this.TargetVertexNumberTextBox.Name = "TargetVertexNumberTextBox";
            this.TargetVertexNumberTextBox.Size = new System.Drawing.Size(88, 20);
            this.TargetVertexNumberTextBox.TabIndex = 11;
            // 
            // StartVertexNumberTextBox
            // 
            this.StartVertexNumberTextBox.Location = new System.Drawing.Point(567, 389);
            this.StartVertexNumberTextBox.Name = "StartVertexNumberTextBox";
            this.StartVertexNumberTextBox.Size = new System.Drawing.Size(88, 20);
            this.StartVertexNumberTextBox.TabIndex = 12;
            // 
            // StartVertexLabel
            // 
            this.StartVertexLabel.AutoSize = true;
            this.StartVertexLabel.Location = new System.Drawing.Point(564, 370);
            this.StartVertexLabel.Name = "StartVertexLabel";
            this.StartVertexLabel.Size = new System.Drawing.Size(89, 13);
            this.StartVertexLabel.TabIndex = 13;
            this.StartVertexLabel.Text = "Вершина старта";
            // 
            // TargetVertexLabel
            // 
            this.TargetVertexLabel.AutoSize = true;
            this.TargetVertexLabel.Location = new System.Drawing.Point(670, 370);
            this.TargetVertexLabel.Name = "TargetVertexLabel";
            this.TargetVertexLabel.Size = new System.Drawing.Size(95, 13);
            this.TargetVertexLabel.TabIndex = 14;
            this.TargetVertexLabel.Text = "Вершина финиша";
            // 
            // SearchAllWaysButton
            // 
            this.SearchAllWaysButton.Location = new System.Drawing.Point(567, 344);
            this.SearchAllWaysButton.Name = "SearchAllWaysButton";
            this.SearchAllWaysButton.Size = new System.Drawing.Size(194, 23);
            this.SearchAllWaysButton.TabIndex = 15;
            this.SearchAllWaysButton.Text = "Найти все пути";
            this.SearchAllWaysButton.UseVisualStyleBackColor = true;
            this.SearchAllWaysButton.Click += new System.EventHandler(this.SearchAllWaysButton_Click);
            // 
            // ListAllWaysListBox
            // 
            this.ListAllWaysListBox.FormattingEnabled = true;
            this.ListAllWaysListBox.Location = new System.Drawing.Point(567, 55);
            this.ListAllWaysListBox.Name = "ListAllWaysListBox";
            this.ListAllWaysListBox.Size = new System.Drawing.Size(221, 212);
            this.ListAllWaysListBox.TabIndex = 16;
            // 
            // ListAllWaysLabel
            // 
            this.ListAllWaysLabel.AutoSize = true;
            this.ListAllWaysLabel.Location = new System.Drawing.Point(564, 38);
            this.ListAllWaysLabel.Name = "ListAllWaysLabel";
            this.ListAllWaysLabel.Size = new System.Drawing.Size(131, 13);
            this.ListAllWaysLabel.TabIndex = 17;
            this.ListAllWaysLabel.Text = "Старт - финиш (все пути)";
            // 
            // GraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ListAllWaysLabel);
            this.Controls.Add(this.ListAllWaysListBox);
            this.Controls.Add(this.SearchAllWaysButton);
            this.Controls.Add(this.TargetVertexLabel);
            this.Controls.Add(this.StartVertexLabel);
            this.Controls.Add(this.StartVertexNumberTextBox);
            this.Controls.Add(this.TargetVertexNumberTextBox);
            this.Controls.Add(this.FindShortWayButton);
            this.Controls.Add(this.DeleteElementButton);
            this.Controls.Add(this.DrawEdgeButton);
            this.Controls.Add(this.DrawVertexButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GraphWindow";
            this.Text = "Algorithm Lee";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button DrawVertexButton;
        private System.Windows.Forms.Button DrawEdgeButton;
        private System.Windows.Forms.Button DeleteElementButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearAllGraphToolStripMenuItem;
        private System.Windows.Forms.Button FindShortWayButton;
        private System.Windows.Forms.TextBox TargetVertexNumberTextBox;
        private System.Windows.Forms.TextBox StartVertexNumberTextBox;
        private System.Windows.Forms.Label StartVertexLabel;
        private System.Windows.Forms.Label TargetVertexLabel;
        private System.Windows.Forms.Button SearchAllWaysButton;
        private System.Windows.Forms.ListBox ListAllWaysListBox;
        private System.Windows.Forms.Label ListAllWaysLabel;
    }
}

