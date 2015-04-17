namespace CompareBytes
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFolder1 = new System.Windows.Forms.Button();
            this.btnFolder2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbFolder1 = new System.Windows.Forms.TextBox();
            this.txbFolder2 = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnFolder1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFolder2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbFolder1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbFolder2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnGo, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(590, 113);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnFolder1
            // 
            this.btnFolder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFolder1.Location = new System.Drawing.Point(298, 3);
            this.btnFolder1.Name = "btnFolder1";
            this.btnFolder1.Size = new System.Drawing.Size(289, 31);
            this.btnFolder1.TabIndex = 0;
            this.btnFolder1.Text = "Folder 1...";
            this.btnFolder1.UseVisualStyleBackColor = true;
            this.btnFolder1.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // btnFolder2
            // 
            this.btnFolder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFolder2.Location = new System.Drawing.Point(298, 40);
            this.btnFolder2.Name = "btnFolder2";
            this.btnFolder2.Size = new System.Drawing.Size(289, 31);
            this.btnFolder2.TabIndex = 0;
            this.btnFolder2.Text = "Folder 2...";
            this.btnFolder2.UseVisualStyleBackColor = true;
            this.btnFolder2.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "-----";
            // 
            // txbFolder1
            // 
            this.txbFolder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbFolder1.Location = new System.Drawing.Point(3, 3);
            this.txbFolder1.Name = "txbFolder1";
            this.txbFolder1.Size = new System.Drawing.Size(289, 22);
            this.txbFolder1.TabIndex = 2;
            // 
            // txbFolder2
            // 
            this.txbFolder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbFolder2.Location = new System.Drawing.Point(3, 40);
            this.txbFolder2.Name = "txbFolder2";
            this.txbFolder2.Size = new System.Drawing.Size(289, 22);
            this.txbFolder2.TabIndex = 2;
            // 
            // btnGo
            // 
            this.btnGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGo.Location = new System.Drawing.Point(298, 77);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(289, 33);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 113);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnFolder1;
        private System.Windows.Forms.Button btnFolder2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbFolder1;
        private System.Windows.Forms.TextBox txbFolder2;
        private System.Windows.Forms.Button btnGo;
    }
}

