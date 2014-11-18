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
            this.chbSpeedOverReports = new System.Windows.Forms.CheckBox();
            this.chbPerformReverseTransform = new System.Windows.Forms.CheckBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pbTransform = new System.Windows.Forms.ProgressBar();
            this.btnInexactSearch = new System.Windows.Forms.Button();
            this.bchReverse = new BWT.BwtBenchmarkingPanel();
            this.bchBwt = new BWT.BwtBenchmarkingPanel();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblErrorsAllowed = new System.Windows.Forms.Label();
            this.nupErrorsAllowed = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tpAlgorithm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLed)).BeginInit();
            this.tpIntermediates.SuspendLayout();
            this.tpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorsAllowed)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAlgorithm);
            this.tabControl1.Controls.Add(this.tpIntermediates);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(879, 753);
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
            this.tpAlgorithm.Location = new System.Drawing.Point(4, 25);
            this.tpAlgorithm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpAlgorithm.Name = "tpAlgorithm";
            this.tpAlgorithm.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpAlgorithm.Size = new System.Drawing.Size(871, 724);
            this.tpAlgorithm.TabIndex = 0;
            this.tpAlgorithm.Text = "Algorithm Execution";
            this.tpAlgorithm.UseVisualStyleBackColor = true;
            // 
            // pbLed
            // 
            this.pbLed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLed.ErrorImage = null;
            this.pbLed.Image = global::BWT.Properties.Resources.green_led;
            this.pbLed.Location = new System.Drawing.Point(804, 511);
            this.pbLed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbLed.Name = "pbLed";
            this.pbLed.Size = new System.Drawing.Size(27, 23);
            this.pbLed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLed.TabIndex = 3;
            this.pbLed.TabStop = false;
            this.pbLed.Visible = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(13, 219);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(129, 25);
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
            this.lblSeparator.Location = new System.Drawing.Point(11, 258);
            this.lblSeparator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(849, 2);
            this.lblSeparator.TabIndex = 1;
            // 
            // lblReversedOutput
            // 
            this.lblReversedOutput.AutoSize = true;
            this.lblReversedOutput.Location = new System.Drawing.Point(12, 522);
            this.lblReversedOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReversedOutput.Name = "lblReversedOutput";
            this.lblReversedOutput.Size = new System.Drawing.Size(120, 17);
            this.lblReversedOutput.TabIndex = 1;
            this.lblReversedOutput.Text = "Reversed Output:";
            // 
            // lblOutPut
            // 
            this.lblOutPut.AutoSize = true;
            this.lblOutPut.Location = new System.Drawing.Point(11, 314);
            this.lblOutPut.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutPut.Name = "lblOutPut";
            this.lblOutPut.Size = new System.Drawing.Size(55, 17);
            this.lblOutPut.TabIndex = 1;
            this.lblOutPut.Text = "Output:";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(11, 20);
            this.lblInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(43, 17);
            this.lblInput.TabIndex = 1;
            this.lblInput.Text = "Input:";
            // 
            // txbReversedOutput
            // 
            this.txbReversedOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReversedOutput.Location = new System.Drawing.Point(16, 542);
            this.txbReversedOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbReversedOutput.Multiline = true;
            this.txbReversedOutput.Name = "txbReversedOutput";
            this.txbReversedOutput.ReadOnly = true;
            this.txbReversedOutput.Size = new System.Drawing.Size(816, 158);
            this.txbReversedOutput.TabIndex = 0;
            // 
            // txbOutPut
            // 
            this.txbOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbOutPut.Location = new System.Drawing.Point(13, 334);
            this.txbOutPut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbOutPut.Multiline = true;
            this.txbOutPut.Name = "txbOutPut";
            this.txbOutPut.ReadOnly = true;
            this.txbOutPut.Size = new System.Drawing.Size(816, 158);
            this.txbOutPut.TabIndex = 0;
            // 
            // txbInput
            // 
            this.txbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbInput.Location = new System.Drawing.Point(13, 39);
            this.txbInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbInput.Multiline = true;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(832, 171);
            this.txbInput.TabIndex = 0;
            this.txbInput.TextChanged += new System.EventHandler(this.txbInput_TextChanged);
            this.txbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbInput_KeyDown);
            // 
            // tpIntermediates
            // 
            this.tpIntermediates.Controls.Add(this.txbIntermediates);
            this.tpIntermediates.Location = new System.Drawing.Point(4, 25);
            this.tpIntermediates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpIntermediates.Name = "tpIntermediates";
            this.tpIntermediates.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpIntermediates.Size = new System.Drawing.Size(871, 724);
            this.tpIntermediates.TabIndex = 2;
            this.tpIntermediates.Text = "Intermediate states";
            this.tpIntermediates.UseVisualStyleBackColor = true;
            // 
            // txbIntermediates
            // 
            this.txbIntermediates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbIntermediates.Location = new System.Drawing.Point(4, 4);
            this.txbIntermediates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbIntermediates.Multiline = true;
            this.txbIntermediates.Name = "txbIntermediates";
            this.txbIntermediates.ReadOnly = true;
            this.txbIntermediates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbIntermediates.Size = new System.Drawing.Size(863, 716);
            this.txbIntermediates.TabIndex = 0;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.nupErrorsAllowed);
            this.tpSettings.Controls.Add(this.lblErrorsAllowed);
            this.tpSettings.Controls.Add(this.lblSearch);
            this.tpSettings.Controls.Add(this.txbSearch);
            this.tpSettings.Controls.Add(this.btnInexactSearch);
            this.tpSettings.Controls.Add(this.lnkWikipedia);
            this.tpSettings.Controls.Add(this.chbSpeedOverReports);
            this.tpSettings.Controls.Add(this.chbPerformReverseTransform);
            this.tpSettings.Location = new System.Drawing.Point(4, 25);
            this.tpSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSettings.Size = new System.Drawing.Size(871, 724);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // lnkWikipedia
            // 
            this.lnkWikipedia.AutoSize = true;
            this.lnkWikipedia.Location = new System.Drawing.Point(11, 11);
            this.lnkWikipedia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkWikipedia.Name = "lnkWikipedia";
            this.lnkWikipedia.Size = new System.Drawing.Size(270, 17);
            this.lnkWikipedia.TabIndex = 1;
            this.lnkWikipedia.TabStop = true;
            this.lnkWikipedia.Text = "Burrows–Wheeler transform on Wikiperdia";
            this.lnkWikipedia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWikipedia_LinkClicked);
            // 
            // chbSpeedOverReports
            // 
            this.chbSpeedOverReports.AutoSize = true;
            this.chbSpeedOverReports.Checked = true;
            this.chbSpeedOverReports.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSpeedOverReports.Location = new System.Drawing.Point(11, 59);
            this.chbSpeedOverReports.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chbSpeedOverReports.Name = "chbSpeedOverReports";
            this.chbSpeedOverReports.Size = new System.Drawing.Size(447, 21);
            this.chbSpeedOverReports.TabIndex = 0;
            this.chbSpeedOverReports.Text = "Send less detailed progress reports in order to speed up progress";
            this.chbSpeedOverReports.UseVisualStyleBackColor = true;
            this.chbSpeedOverReports.CheckedChanged += new System.EventHandler(this.chbSpeedOverReports_CheckedChanged);
            // 
            // chbPerformReverseTransform
            // 
            this.chbPerformReverseTransform.AutoSize = true;
            this.chbPerformReverseTransform.Checked = true;
            this.chbPerformReverseTransform.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPerformReverseTransform.Location = new System.Drawing.Point(11, 31);
            this.chbPerformReverseTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chbPerformReverseTransform.Name = "chbPerformReverseTransform";
            this.chbPerformReverseTransform.Size = new System.Drawing.Size(346, 21);
            this.chbPerformReverseTransform.TabIndex = 0;
            this.chbPerformReverseTransform.Text = "Perform Reverse Transform when BWT completes";
            this.chbPerformReverseTransform.UseVisualStyleBackColor = true;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.scMain.Size = new System.Drawing.Size(879, 789);
            this.scMain.SplitterDistance = 753;
            this.scMain.SplitterWidth = 5;
            this.scMain.TabIndex = 1;
            // 
            // pbTransform
            // 
            this.pbTransform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTransform.Location = new System.Drawing.Point(0, 0);
            this.pbTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbTransform.Name = "pbTransform";
            this.pbTransform.Size = new System.Drawing.Size(879, 31);
            this.pbTransform.TabIndex = 0;
            // 
            // btnInexactSearch
            // 
            this.btnInexactSearch.Location = new System.Drawing.Point(82, 165);
            this.btnInexactSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnInexactSearch.Name = "btnInexactSearch";
            this.btnInexactSearch.Size = new System.Drawing.Size(184, 25);
            this.btnInexactSearch.TabIndex = 3;
            this.btnInexactSearch.Text = "Execute Inexact Search!";
            this.btnInexactSearch.UseVisualStyleBackColor = true;
            this.btnInexactSearch.Click += new System.EventHandler(this.btnInexactSearch_Click);
            // 
            // bchReverse
            // 
            this.bchReverse.Location = new System.Drawing.Point(260, 500);
            this.bchReverse.Margin = new System.Windows.Forms.Padding(5);
            this.bchReverse.Name = "bchReverse";
            this.bchReverse.Precision = 8;
            this.bchReverse.Size = new System.Drawing.Size(536, 38);
            this.bchReverse.TabIndex = 4;
            this.bchReverse.Visible = false;
            // 
            // bchBwt
            // 
            this.bchBwt.Location = new System.Drawing.Point(260, 292);
            this.bchBwt.Margin = new System.Windows.Forms.Padding(5);
            this.bchBwt.Name = "bchBwt";
            this.bchBwt.Precision = 8;
            this.bchBwt.Size = new System.Drawing.Size(536, 38);
            this.bchBwt.TabIndex = 4;
            this.bchBwt.Visible = false;
            // 
            // txbSearch
            // 
            this.txbSearch.Location = new System.Drawing.Point(181, 108);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(85, 22);
            this.txbSearch.TabIndex = 4;
            this.txbSearch.Text = "lol";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(8, 111);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(108, 17);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "String to search";
            // 
            // lblErrorsAllowed
            // 
            this.lblErrorsAllowed.AutoSize = true;
            this.lblErrorsAllowed.Location = new System.Drawing.Point(8, 138);
            this.lblErrorsAllowed.Name = "lblErrorsAllowed";
            this.lblErrorsAllowed.Size = new System.Drawing.Size(167, 17);
            this.lblErrorsAllowed.TabIndex = 5;
            this.lblErrorsAllowed.Text = "Number of errors allowed";
            // 
            // nupErrorsAllowed
            // 
            this.nupErrorsAllowed.Location = new System.Drawing.Point(181, 136);
            this.nupErrorsAllowed.Name = "nupErrorsAllowed";
            this.nupErrorsAllowed.Size = new System.Drawing.Size(85, 22);
            this.nupErrorsAllowed.TabIndex = 6;
            this.nupErrorsAllowed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // frmBwt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 789);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorsAllowed)).EndInit();
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
        private System.Windows.Forms.Button btnInexactSearch;
        private System.Windows.Forms.NumericUpDown nupErrorsAllowed;
        private System.Windows.Forms.Label lblErrorsAllowed;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txbSearch;

    }
}

