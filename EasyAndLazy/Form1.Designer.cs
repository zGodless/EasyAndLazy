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
            this.textEdit1 = new System.Windows.Forms.Label();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.textEdit1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.MaximumSize = new System.Drawing.Size(1492, 20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(1492, 15);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.Text = "label1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.BackColor = System.Drawing.Color.Transparent;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Location = new System.Drawing.Point(1472, 0);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(0);
            this.simpleButton1.MaximumSize = new System.Drawing.Size(20, 20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(20, 15);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "button1";
            this.simpleButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 15);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.2D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label textEdit1;
        private System.Windows.Forms.Button simpleButton1;
    }
}

