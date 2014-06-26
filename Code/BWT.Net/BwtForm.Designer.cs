namespace BWT
{
    partial class frmBwt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBwt));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAlgorithm = new System.Windows.Forms.TabPage();
            this.pbLed = new System.Windows.Forms.PictureBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblReversedOutput = new System.Windows.Forms.Label();
            this.lblOutPut = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.txbReversedOutput = new System.Windows.Forms.TextBox();
            this.txbOutPut = new System.Windows.Forms.TextBox();
            this.txbInput = new System.Windows.Forms.TextBox();
            this.tpIntermediates = new System.Windows.Forms.TabPage();
            this.txbIntermediates = new System.Windows.Forms.TextBox();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.lnkWikipedia = new System.Windows.Forms.LinkLabel();
            this.chbPerformReverseTransform = new System.Windows.Forms.CheckBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pbTransform = new System.Windows.Forms.ProgressBar();
            this.bchReverse = new BWT.BwtBenchmarkingPanel();
            this.bchBwt = new BWT.BwtBenchmarkingPanel();
            this.chbSpeedOverReports = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tpAlgorithm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLed)).BeginInit();
            this.tpIntermediates.SuspendLayout();
            this.tpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAlgorithm);
            this.tabControl1.Controls.Add(this.tpIntermediates);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(659, 612);
            this.tabControl1.TabIndex = 0;
            // 
            // tpAlgorithm
            // 
            this.tpAlgorithm.Controls.Add(this.bchReverse);
            this.tpAlgorithm.Controls.Add(this.bchBwt);
            this.tpAlgorithm.Controls.Add(this.pbLed);
            this.tpAlgorithm.Controls.Add(this.btnExecute);
            this.tpAlgorithm.Controls.Add(this.lblSeparator);
            this.tpAlgorithm.Controls.Add(this.lblReversedOutput);
            this.tpAlgorithm.Controls.Add(this.lblOutPut);
            this.tpAlgorithm.Controls.Add(this.lblInput);
            this.tpAlgorithm.Controls.Add(this.txbReversedOutput);
            this.tpAlgorithm.Controls.Add(this.txbOutPut);
            this.tpAlgorithm.Controls.Add(this.txbInput);
            this.tpAlgorithm.Location = new System.Drawing.Point(4, 22);
            this.tpAlgorithm.Name = "tpAlgorithm";
            this.tpAlgorithm.Padding = new System.Windows.Forms.Padding(3);
            this.tpAlgorithm.Size = new System.Drawing.Size(651, 586);
            this.tpAlgorithm.TabIndex = 0;
            this.tpAlgorithm.Text = "Algorithm Execution";
            this.tpAlgorithm.UseVisualStyleBackColor = true;
            // 
            // pbLed
            // 
            this.pbLed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLed.ErrorImage = null;
            this.pbLed.Image = global::BWT.Properties.Resources.green_led;
            this.pbLed.Location = new System.Drawing.Point(603, 415);
            this.pbLed.Name = "pbLed";
            this.pbLed.Size = new System.Drawing.Size(20, 19);
            this.pbLed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLed.TabIndex = 3;
            this.pbLed.TabStop = false;
            this.pbLed.Visible = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(10, 178);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(97, 20);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Execute!";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // lblSeparator
            // 
            this.lblSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(8, 210);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(637, 2);
            this.lblSeparator.TabIndex = 1;
            // 
            // lblReversedOutput
            // 
            this.lblReversedOutput.AutoSize = true;
            this.lblReversedOutput.Location = new System.Drawing.Point(9, 424);
            this.lblReversedOutput.Name = "lblReversedOutput";
            this.lblReversedOutput.Size = new System.Drawing.Size(91, 13);
            this.lblReversedOutput.TabIndex = 1;
            this.lblReversedOutput.Text = "Reversed Output:";
            // 
            // lblOutPut
            // 
            this.lblOutPut.AutoSize = true;
            this.lblOutPut.Location = new System.Drawing.Point(8, 255);
            this.lblOutPut.Name = "lblOutPut";
            this.lblOutPut.Size = new System.Drawing.Size(42, 13);
            this.lblOutPut.TabIndex = 1;
            this.lblOutPut.Text = "Output:";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(8, 16);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(34, 13);
            this.lblInput.TabIndex = 1;
            this.lblInput.Text = "Input:";
            // 
            // txbReversedOutput
            // 
            this.txbReversedOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReversedOutput.Location = new System.Drawing.Point(12, 440);
            this.txbReversedOutput.Multiline = true;
            this.txbReversedOutput.Name = "txbReversedOutput";
            this.txbReversedOutput.ReadOnly = true;
            this.txbReversedOutput.Size = new System.Drawing.Size(613, 129);
            this.txbReversedOutput.TabIndex = 0;
            // 
            // txbOutPut
            // 
            this.txbOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbOutPut.Location = new System.Drawing.Point(10, 271);
            this.txbOutPut.Multiline = true;
            this.txbOutPut.Name = "txbOutPut";
            this.txbOutPut.ReadOnly = true;
            this.txbOutPut.Size = new System.Drawing.Size(613, 129);
            this.txbOutPut.TabIndex = 0;
            // 
            // txbInput
            // 
            this.txbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbInput.Location = new System.Drawing.Point(10, 32);
            this.txbInput.Multiline = true;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(625, 140);
            this.txbInput.TabIndex = 0;
            this.txbInput.TextChanged += new System.EventHandler(this.txbInput_TextChanged);
            this.txbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbInput_KeyDown);
            // 
            // tpIntermediates
            // 
            this.tpIntermediates.Controls.Add(this.txbIntermediates);
            this.tpIntermediates.Location = new System.Drawing.Point(4, 22);
            this.tpIntermediates.Name = "tpIntermediates";
            this.tpIntermediates.Padding = new System.Windows.Forms.Padding(3);
            this.tpIntermediates.Size = new System.Drawing.Size(651, 586);
            this.tpIntermediates.TabIndex = 2;
            this.tpIntermediates.Text = "Intermediate states";
            this.tpIntermediates.UseVisualStyleBackColor = true;
            // 
            // txbIntermediates
            // 
            this.txbIntermediates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbIntermediates.Location = new System.Drawing.Point(3, 3);
            this.txbIntermediates.Multiline = true;
            this.txbIntermediates.Name = "txbIntermediates";
            this.txbIntermediates.ReadOnly = true;
            this.txbIntermediates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbIntermediates.Size = new System.Drawing.Size(645, 580);
            this.txbIntermediates.TabIndex = 0;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.lnkWikipedia);
            this.tpSettings.Controls.Add(this.chbSpeedOverReports);
            this.tpSettings.Controls.Add(this.chbPerformReverseTransform);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(651, 586);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // lnkWikipedia
            // 
            this.lnkWikipedia.AutoSize = true;
            this.lnkWikipedia.Location = new System.Drawing.Point(8, 9);
            this.lnkWikipedia.Name = "lnkWikipedia";
            this.lnkWikipedia.Size = new System.Drawing.Size(204, 13);
            this.lnkWikipedia.TabIndex = 1;
            this.lnkWikipedia.TabStop = true;
            this.lnkWikipedia.Text = "Burrows–Wheeler transform on Wikiperdia";
            this.lnkWikipedia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWikipedia_LinkClicked);
            // 
            // chbPerformReverseTransform
            // 
            this.chbPerformReverseTransform.AutoSize = true;
            this.chbPerformReverseTransform.Checked = true;
            this.chbPerformReverseTransform.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPerformReverseTransform.Location = new System.Drawing.Point(8, 25);
            this.chbPerformReverseTransform.Name = "chbPerformReverseTransform";
            this.chbPerformReverseTransform.Size = new System.Drawing.Size(263, 17);
            this.chbPerformReverseTransform.TabIndex = 0;
            this.chbPerformReverseTransform.Text = "Perform Reverse Transform when BWT completes";
            this.chbPerformReverseTransform.UseVisualStyleBackColor = true;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tabControl1);
            this.scMain.Panel1MinSize = 500;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.pbTransform);
            this.scMain.Panel2MinSize = 10;
            this.scMain.Size = new System.Drawing.Size(659, 641);
            this.scMain.SplitterDistance = 612;
            this.scMain.TabIndex = 1;
            // 
            // pbTransform
            // 
            this.pbTransform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTransform.Location = new System.Drawing.Point(0, 0);
            this.pbTransform.Name = "pbTransform";
            this.pbTransform.Size = new System.Drawing.Size(659, 25);
            this.pbTransform.TabIndex = 0;
            // 
            // bchReverse
            // 
            this.bchReverse.Location = new System.Drawing.Point(195, 406);
            this.bchReverse.Name = "bchReverse";
            this.bchReverse.Precision = 8;
            this.bchReverse.Size = new System.Drawing.Size(402, 31);
            this.bchReverse.TabIndex = 4;
            this.bchReverse.Visible = false;
            // 
            // bchBwt
            // 
            this.bchBwt.Location = new System.Drawing.Point(195, 237);
            this.bchBwt.Name = "bchBwt";
            this.bchBwt.Precision = 8;
            this.bchBwt.Size = new System.Drawing.Size(402, 31);
            this.bchBwt.TabIndex = 4;
            this.bchBwt.Visible = false;
            // 
            // chbSpeedOverReports
            // 
            this.chbSpeedOverReports.AutoSize = true;
            this.chbSpeedOverReports.Checked = true;
            this.chbSpeedOverReports.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSpeedOverReports.Location = new System.Drawing.Point(8, 48);
            this.chbSpeedOverReports.Name = "chbSpeedOverReports";
            this.chbSpeedOverReports.Size = new System.Drawing.Size(330, 17);
            this.chbSpeedOverReports.TabIndex = 0;
            this.chbSpeedOverReports.Text = "Send less detailed progress reports in order to speed up progress";
            this.chbSpeedOverReports.UseVisualStyleBackColor = true;
            this.chbSpeedOverReports.CheckedChanged += new System.EventHandler(this.chbSpeedOverReports_CheckedChanged);
            // 
            // frmBwt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 641);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBwt";
            this.Text = "Burrows–Wheeler transform";
            this.tabControl1.ResumeLayout(false);
            this.tpAlgorithm.ResumeLayout(false);
            this.tpAlgorithm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLed)).EndInit();
            this.tpIntermediates.ResumeLayout(false);
            this.tpIntermediates.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAlgorithm;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblOutPut;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txbOutPut;
        private System.Windows.Forms.TextBox txbInput;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Label lblReversedOutput;
        private System.Windows.Forms.TextBox txbReversedOutput;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.CheckBox chbPerformReverseTransform;
        private System.Windows.Forms.TabPage tpIntermediates;
        private System.Windows.Forms.TextBox txbIntermediates;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ProgressBar pbTransform;
        private System.Windows.Forms.PictureBox pbLed;
        private BwtBenchmarkingPanel bchBwt;
        private BwtBenchmarkingPanel bchReverse;
        private System.Windows.Forms.LinkLabel lnkWikipedia;
        private System.Windows.Forms.CheckBox chbSpeedOverReports;

    }
}

