namespace BWT
{
    partial class TextWindow
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
            this.txb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txb
            // 
            this.txb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txb.Location = new System.Drawing.Point(0, 0);
            this.txb.Multiline = true;
            this.txb.Name = "txb";
            this.txb.ReadOnly = true;
            this.txb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb.Size = new System.Drawing.Size(636, 418);
            this.txb.TabIndex = 0;
            this.txb.WordWrap = false;
            // 
            // TextWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 418);
            this.Controls.Add(this.txb);
            this.Name = "TextWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txb;


    }
}