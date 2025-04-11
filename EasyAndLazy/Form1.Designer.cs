namespace EasyAndLazy
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.textEdit1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Location = new System.Drawing.Point(980, 0);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton1.MaximumSize = new System.Drawing.Size(20, 20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(20, 18);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "button1";
            this.simpleButton1.UseVisualStyleBackColor = true;
            // 
            // textEdit1
            // 
            this.textEdit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.textEdit1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEdit1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.MaximumSize = new System.Drawing.Size(1492, 30);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(977, 18);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.Text = "label1";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox.Location = new System.Drawing.Point(2, -2);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(975, 17);
            this.textBox.TabIndex = 2;
            this.textBox.Visible = false;
            // 
            // labelPercent
            // 
            this.labelPercent.AutoSize = true;
            this.labelPercent.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelPercent.Location = new System.Drawing.Point(876, 0);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(53, 12);
            this.labelPercent.TabIndex = 3;
            this.labelPercent.Text = "0%   0/0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(41)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1000, 18);
            this.Controls.Add(this.labelPercent);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.2D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.Label textEdit1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labelPercent;
    }
}

