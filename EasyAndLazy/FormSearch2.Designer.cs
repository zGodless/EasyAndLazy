namespace EasyAndLazy
{
    partial class FormSearch2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.gvSearch = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gvSearch);
            this.panel1.Controls.Add(this.textSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 321);
            this.panel1.TabIndex = 0;
            // 
            // textSearch
            // 
            this.textSearch.Location = new System.Drawing.Point(0, 0);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(631, 21);
            this.textSearch.TabIndex = 0;
            // 
            // gvSearch
            // 
            this.gvSearch.BackgroundColor = System.Drawing.Color.White;
            this.gvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.TextString});
            this.gvSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvSearch.Location = new System.Drawing.Point(0, 22);
            this.gvSearch.Name = "gvSearch";
            this.gvSearch.RowTemplate.Height = 23;
            this.gvSearch.Size = new System.Drawing.Size(631, 299);
            this.gvSearch.TabIndex = 1;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.HeaderText = "Column1";
            this.Index.Name = "Index";
            // 
            // TextString
            // 
            this.TextString.DataPropertyName = "TextString";
            this.TextString.HeaderText = "Column1";
            this.TextString.Name = "TextString";
            this.TextString.Width = 450;
            // 
            // FormSearch2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 321);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSearch2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextString;
    }
}