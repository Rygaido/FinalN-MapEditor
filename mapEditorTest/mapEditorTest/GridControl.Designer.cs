namespace mapEditorTest
{
    partial class GridControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rowBox = new System.Windows.Forms.TextBox();
            this.colBox = new System.Windows.Forms.TextBox();
            this.colLab = new System.Windows.Forms.Label();
            this.rowLab = new System.Windows.Forms.Label();
            this.nameLab = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rowBox
            // 
            this.rowBox.Location = new System.Drawing.Point(12, 25);
            this.rowBox.Name = "rowBox";
            this.rowBox.Size = new System.Drawing.Size(100, 20);
            this.rowBox.TabIndex = 0;
            // 
            // colBox
            // 
            this.colBox.Location = new System.Drawing.Point(118, 25);
            this.colBox.Name = "colBox";
            this.colBox.Size = new System.Drawing.Size(100, 20);
            this.colBox.TabIndex = 1;
            // 
            // colLab
            // 
            this.colLab.AutoSize = true;
            this.colLab.BackColor = System.Drawing.SystemColors.Control;
            this.colLab.Location = new System.Drawing.Point(116, 9);
            this.colLab.Name = "colLab";
            this.colLab.Size = new System.Drawing.Size(47, 13);
            this.colLab.TabIndex = 2;
            this.colLab.Text = "Columns";
            // 
            // rowLab
            // 
            this.rowLab.AutoSize = true;
            this.rowLab.Location = new System.Drawing.Point(12, 9);
            this.rowLab.Name = "rowLab";
            this.rowLab.Size = new System.Drawing.Size(34, 13);
            this.rowLab.TabIndex = 3;
            this.rowLab.Text = "Rows";
            // 
            // nameLab
            // 
            this.nameLab.AutoSize = true;
            this.nameLab.Location = new System.Drawing.Point(15, 70);
            this.nameLab.Name = "nameLab";
            this.nameLab.Size = new System.Drawing.Size(59, 13);
            this.nameLab.TabIndex = 4;
            this.nameLab.Text = "Map Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(12, 87);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(206, 20);
            this.nameBox.TabIndex = 5;
            this.nameBox.Text = ".txt";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(119, 113);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(99, 23);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Save Map";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(120, 142);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(99, 23);
            this.loadBtn.TabIndex = 7;
            this.loadBtn.Text = "Load Map";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(120, 171);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(98, 23);
            this.newBtn.TabIndex = 8;
            this.newBtn.Text = "New Map";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // GridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 203);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.nameLab);
            this.Controls.Add(this.rowLab);
            this.Controls.Add(this.colLab);
            this.Controls.Add(this.colBox);
            this.Controls.Add(this.rowBox);
            this.Name = "GridControl";
            this.Text = "GridControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox rowBox;
        private System.Windows.Forms.TextBox colBox;
        private System.Windows.Forms.Label colLab;
        private System.Windows.Forms.Label rowLab;
        private System.Windows.Forms.Label nameLab;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button newBtn;
    }
}