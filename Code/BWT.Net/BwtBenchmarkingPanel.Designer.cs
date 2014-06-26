namespace BWT
{
    partial class BwtBenchmarkingPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTimePerChar = new System.Windows.Forms.Label();
            this.lblBwtTime = new System.Windows.Forms.Label();
            this.txbBwtTime = new System.Windows.Forms.TextBox();
            this.txbTimePerChar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTimePerChar
            // 
            this.lblTimePerChar.AutoSize = true;
            this.lblTimePerChar.Location = new System.Drawing.Point(181, 10);
            this.lblTimePerChar.Name = "lblTimePerChar";
            this.lblTimePerChar.Size = new System.Drawing.Size(109, 13);
            this.lblTimePerChar.TabIndex = 2;
            this.lblTimePerChar.Text = "Seconds/Charechtar:";
            // 
            // lblBwtTime
            // 
            this.lblBwtTime.AutoSize = true;
            this.lblBwtTime.Location = new System.Drawing.Point(15, 10);
            this.lblBwtTime.Name = "lblBwtTime";
            this.lblBwtTime.Size = new System.Drawing.Size(33, 13);
            this.lblBwtTime.TabIndex = 3;
            this.lblBwtTime.Text = "Time:";
            // 
            // txbBwtTime
            // 
            this.txbBwtTime.Location = new System.Drawing.Point(54, 7);
            this.txbBwtTime.Name = "txbBwtTime";
            this.txbBwtTime.ReadOnly = true;
            this.txbBwtTime.Size = new System.Drawing.Size(94, 20);
            this.txbBwtTime.TabIndex = 4;
            // 
            // txbTimePerChar
            // 
            this.txbTimePerChar.Location = new System.Drawing.Point(296, 7);
            this.txbTimePerChar.Name = "txbTimePerChar";
            this.txbTimePerChar.ReadOnly = true;
            this.txbTimePerChar.Size = new System.Drawing.Size(94, 20);
            this.txbTimePerChar.TabIndex = 4;
            // 
            // BwtBenchmarkingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txbTimePerChar);
            this.Controls.Add(this.txbBwtTime);
            this.Controls.Add(this.lblTimePerChar);
            this.Controls.Add(this.lblBwtTime);
            this.Name = "BwtBenchmarkingPanel";
            this.Size = new System.Drawing.Size(404, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTimePerChar;
        private System.Windows.Forms.Label lblBwtTime;
        private System.Windows.Forms.TextBox txbBwtTime;
        private System.Windows.Forms.TextBox txbTimePerChar;
    }
}
