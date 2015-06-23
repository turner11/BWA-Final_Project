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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpMultipleBwa = new System.Windows.Forms.TabPage();
            this.lblRecommendedMaxError = new System.Windows.Forms.Label();
            this.txbMultiBwaResults = new System.Windows.Forms.TextBox();
            this.rdvBwaBoth = new System.Windows.Forms.RadioButton();
            this.rdvBwaMultipleThread = new System.Windows.Forms.RadioButton();
            this.rdvBwaSingleThread = new System.Windows.Forms.RadioButton();
            this.btnClearMultiple = new System.Windows.Forms.Button();
            this.btnStartMultipleBwa = new System.Windows.Forms.Button();
            this.nupNumberOfReads = new System.Windows.Forms.NumericUpDown();
            this.lblNumberOfReads = new System.Windows.Forms.Label();
            this.nupReadLength = new System.Windows.Forms.NumericUpDown();
            this.lblReadLength = new System.Windows.Forms.Label();
            this.nupErrorPercentage = new System.Windows.Forms.NumericUpDown();
            this.lblErrorPercentage = new System.Windows.Forms.Label();
            this.lblReferenceMirror = new System.Windows.Forms.Label();
            this.txbReferenceMirror = new System.Windows.Forms.TextBox();
            this.tpBwa = new System.Windows.Forms.TabPage();
            this.chbFindGaps = new System.Windows.Forms.CheckBox();
            this.txbBwaResults = new System.Windows.Forms.TextBox();
            this.nupErrorsAllowed = new System.Windows.Forms.NumericUpDown();
            this.lblErrorsAllowed = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txbReference = new System.Windows.Forms.TextBox();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.btnInexactSearch = new System.Windows.Forms.Button();
            this.tpBwt = new System.Windows.Forms.TabPage();
            this.bchReverse = new BWT.BwtBenchmarkingPanel();
            this.bchBwt = new BWT.BwtBenchmarkingPanel();
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
            this.lnkBwaPaper = new System.Windows.Forms.LinkLabel();
            this.lnkWikipedia = new System.Windows.Forms.LinkLabel();
            this.chbSpeedOverReports = new System.Windows.Forms.CheckBox();
            this.chbPerformReverseTransform = new System.Windows.Forms.CheckBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pbTransform = new System.Windows.Forms.ProgressBar();
            this.tcMain.SuspendLayout();
            this.tpMultipleBwa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupNumberOfReads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupReadLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorPercentage)).BeginInit();
            this.tpBwa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorsAllowed)).BeginInit();
            this.tpBwt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLed)).BeginInit();
            this.tpIntermediates.SuspendLayout();
            this.tpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpMultipleBwa);
            this.tcMain.Controls.Add(this.tpBwa);
            this.tcMain.Controls.Add(this.tpBwt);
            this.tcMain.Controls.Add(this.tpIntermediates);
            this.tcMain.Controls.Add(this.tpSettings);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(868, 523);
            this.tcMain.TabIndex = 0;
            // 
            // tpMultipleBwa
            // 
            this.tpMultipleBwa.Controls.Add(this.lblRecommendedMaxError);
            this.tpMultipleBwa.Controls.Add(this.txbMultiBwaResults);
            this.tpMultipleBwa.Controls.Add(this.rdvBwaBoth);
            this.tpMultipleBwa.Controls.Add(this.rdvBwaMultipleThread);
            this.tpMultipleBwa.Controls.Add(this.rdvBwaSingleThread);
            this.tpMultipleBwa.Controls.Add(this.btnClearMultiple);
            this.tpMultipleBwa.Controls.Add(this.btnStartMultipleBwa);
            this.tpMultipleBwa.Controls.Add(this.nupNumberOfReads);
            this.tpMultipleBwa.Controls.Add(this.lblNumberOfReads);
            this.tpMultipleBwa.Controls.Add(this.nupReadLength);
            this.tpMultipleBwa.Controls.Add(this.lblReadLength);
            this.tpMultipleBwa.Controls.Add(this.nupErrorPercentage);
            this.tpMultipleBwa.Controls.Add(this.lblErrorPercentage);
            this.tpMultipleBwa.Controls.Add(this.lblReferenceMirror);
            this.tpMultipleBwa.Controls.Add(this.txbReferenceMirror);
            this.tpMultipleBwa.Location = new System.Drawing.Point(4, 25);
            this.tpMultipleBwa.Name = "tpMultipleBwa";
            this.tpMultipleBwa.Padding = new System.Windows.Forms.Padding(3);
            this.tpMultipleBwa.Size = new System.Drawing.Size(860, 494);
            this.tpMultipleBwa.TabIndex = 4;
            this.tpMultipleBwa.Text = "Multiple Bwa";
            this.tpMultipleBwa.UseVisualStyleBackColor = true;
            // 
            // lblRecommendedMaxError
            // 
            this.lblRecommendedMaxError.AutoSize = true;
            this.lblRecommendedMaxError.Location = new System.Drawing.Point(268, 169);
            this.lblRecommendedMaxError.Name = "lblRecommendedMaxError";
            this.lblRecommendedMaxError.Size = new System.Drawing.Size(147, 17);
            this.lblRecommendedMaxError.TabIndex = 21;
            this.lblRecommendedMaxError.Text = "Calculated Max Error: ";
            // 
            // txbMultiBwaResults
            // 
            this.txbMultiBwaResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMultiBwaResults.Location = new System.Drawing.Point(8, 261);
            this.txbMultiBwaResults.Multiline = true;
            this.txbMultiBwaResults.Name = "txbMultiBwaResults";
            this.txbMultiBwaResults.ReadOnly = true;
            this.txbMultiBwaResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbMultiBwaResults.Size = new System.Drawing.Size(844, 227);
            this.txbMultiBwaResults.TabIndex = 20;
            // 
            // rdvBwaBoth
            // 
            this.rdvBwaBoth.AutoSize = true;
            this.rdvBwaBoth.Location = new System.Drawing.Point(271, 201);
            this.rdvBwaBoth.Name = "rdvBwaBoth";
            this.rdvBwaBoth.Size = new System.Drawing.Size(58, 21);
            this.rdvBwaBoth.TabIndex = 19;
            this.rdvBwaBoth.Text = "Both";
            this.rdvBwaBoth.UseVisualStyleBackColor = true;
            // 
            // rdvBwaMultipleThread
            // 
            this.rdvBwaMultipleThread.AutoSize = true;
            this.rdvBwaMultipleThread.Location = new System.Drawing.Point(131, 201);
            this.rdvBwaMultipleThread.Name = "rdvBwaMultipleThread";
            this.rdvBwaMultipleThread.Size = new System.Drawing.Size(134, 21);
            this.rdvBwaMultipleThread.TabIndex = 19;
            this.rdvBwaMultipleThread.Text = "Multiple Threads";
            this.rdvBwaMultipleThread.UseVisualStyleBackColor = true;
            // 
            // rdvBwaSingleThread
            // 
            this.rdvBwaSingleThread.AutoSize = true;
            this.rdvBwaSingleThread.Checked = true;
            this.rdvBwaSingleThread.Location = new System.Drawing.Point(11, 201);
            this.rdvBwaSingleThread.Name = "rdvBwaSingleThread";
            this.rdvBwaSingleThread.Size = new System.Drawing.Size(118, 21);
            this.rdvBwaSingleThread.TabIndex = 19;
            this.rdvBwaSingleThread.TabStop = true;
            this.rdvBwaSingleThread.Text = "Single Thread";
            this.rdvBwaSingleThread.UseVisualStyleBackColor = true;
            // 
            // btnClearMultiple
            // 
            this.btnClearMultiple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearMultiple.Location = new System.Drawing.Point(689, 228);
            this.btnClearMultiple.Name = "btnClearMultiple";
            this.btnClearMultiple.Size = new System.Drawing.Size(163, 27);
            this.btnClearMultiple.TabIndex = 18;
            this.btnClearMultiple.Text = "Clear";
            this.btnClearMultiple.UseVisualStyleBackColor = true;
            this.btnClearMultiple.Click += new System.EventHandler(this.btnClearMultiple_Click);
            // 
            // btnStartMultipleBwa
            // 
            this.btnStartMultipleBwa.Location = new System.Drawing.Point(10, 228);
            this.btnStartMultipleBwa.Name = "btnStartMultipleBwa";
            this.btnStartMultipleBwa.Size = new System.Drawing.Size(163, 27);
            this.btnStartMultipleBwa.TabIndex = 18;
            this.btnStartMultipleBwa.Text = "Go!";
            this.btnStartMultipleBwa.UseVisualStyleBackColor = true;
            this.btnStartMultipleBwa.Click += new System.EventHandler(this.btnStartMultipleBwa_Click);
            // 
            // nupNumberOfReads
            // 
            this.nupNumberOfReads.Location = new System.Drawing.Point(131, 139);
            this.nupNumberOfReads.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nupNumberOfReads.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupNumberOfReads.Name = "nupNumberOfReads";
            this.nupNumberOfReads.Size = new System.Drawing.Size(112, 22);
            this.nupNumberOfReads.TabIndex = 17;
            this.nupNumberOfReads.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblNumberOfReads
            // 
            this.lblNumberOfReads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberOfReads.AutoSize = true;
            this.lblNumberOfReads.Location = new System.Drawing.Point(8, 139);
            this.lblNumberOfReads.Name = "lblNumberOfReads";
            this.lblNumberOfReads.Size = new System.Drawing.Size(119, 17);
            this.lblNumberOfReads.TabIndex = 16;
            this.lblNumberOfReads.Text = "Number of Reads";
            // 
            // nupReadLength
            // 
            this.nupReadLength.Location = new System.Drawing.Point(131, 167);
            this.nupReadLength.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nupReadLength.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupReadLength.Name = "nupReadLength";
            this.nupReadLength.Size = new System.Drawing.Size(112, 22);
            this.nupReadLength.TabIndex = 15;
            this.nupReadLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupReadLength.ValueChanged += new System.EventHandler(this.nupReadLength_ValueChanged);
            // 
            // lblReadLength
            // 
            this.lblReadLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReadLength.AutoSize = true;
            this.lblReadLength.Location = new System.Drawing.Point(8, 167);
            this.lblReadLength.Name = "lblReadLength";
            this.lblReadLength.Size = new System.Drawing.Size(90, 17);
            this.lblReadLength.TabIndex = 14;
            this.lblReadLength.Text = "Read Length";
            // 
            // nupErrorPercentage
            // 
            this.nupErrorPercentage.DecimalPlaces = 3;
            this.nupErrorPercentage.Location = new System.Drawing.Point(131, 110);
            this.nupErrorPercentage.Name = "nupErrorPercentage";
            this.nupErrorPercentage.Size = new System.Drawing.Size(112, 22);
            this.nupErrorPercentage.TabIndex = 13;
            this.nupErrorPercentage.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblErrorPercentage
            // 
            this.lblErrorPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblErrorPercentage.AutoSize = true;
            this.lblErrorPercentage.Location = new System.Drawing.Point(8, 110);
            this.lblErrorPercentage.Name = "lblErrorPercentage";
            this.lblErrorPercentage.Size = new System.Drawing.Size(117, 17);
            this.lblErrorPercentage.TabIndex = 12;
            this.lblErrorPercentage.Text = "Error Percentage";
            // 
            // lblReferenceMirror
            // 
            this.lblReferenceMirror.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReferenceMirror.AutoSize = true;
            this.lblReferenceMirror.Location = new System.Drawing.Point(7, 6);
            this.lblReferenceMirror.Name = "lblReferenceMirror";
            this.lblReferenceMirror.Size = new System.Drawing.Size(74, 17);
            this.lblReferenceMirror.TabIndex = 12;
            this.lblReferenceMirror.Text = "Reference";
            // 
            // txbReferenceMirror
            // 
            this.txbReferenceMirror.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReferenceMirror.Location = new System.Drawing.Point(131, 6);
            this.txbReferenceMirror.Multiline = true;
            this.txbReferenceMirror.Name = "txbReferenceMirror";
            this.txbReferenceMirror.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbReferenceMirror.Size = new System.Drawing.Size(721, 96);
            this.txbReferenceMirror.TabIndex = 11;
            this.txbReferenceMirror.Text = "google";
            this.txbReferenceMirror.TextChanged += new System.EventHandler(this.txbReferenceMirror_TextChanged);
            // 
            // tpBwa
            // 
            this.tpBwa.Controls.Add(this.chbFindGaps);
            this.tpBwa.Controls.Add(this.txbBwaResults);
            this.tpBwa.Controls.Add(this.nupErrorsAllowed);
            this.tpBwa.Controls.Add(this.lblErrorsAllowed);
            this.tpBwa.Controls.Add(this.lblReference);
            this.tpBwa.Controls.Add(this.lblSearch);
            this.tpBwa.Controls.Add(this.txbReference);
            this.tpBwa.Controls.Add(this.txbSearch);
            this.tpBwa.Controls.Add(this.btnInexactSearch);
            this.tpBwa.Location = new System.Drawing.Point(4, 25);
            this.tpBwa.Name = "tpBwa";
            this.tpBwa.Size = new System.Drawing.Size(860, 494);
            this.tpBwa.TabIndex = 3;
            this.tpBwa.Text = "BWA";
            this.tpBwa.UseVisualStyleBackColor = true;
            // 
            // chbFindGaps
            // 
            this.chbFindGaps.AutoSize = true;
            this.chbFindGaps.Checked = true;
            this.chbFindGaps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFindGaps.Location = new System.Drawing.Point(11, 88);
            this.chbFindGaps.Name = "chbFindGaps";
            this.chbFindGaps.Size = new System.Drawing.Size(127, 21);
            this.chbFindGaps.TabIndex = 13;
            this.chbFindGaps.Text = "Find gap errors";
            this.chbFindGaps.UseVisualStyleBackColor = true;
            this.chbFindGaps.CheckedChanged += new System.EventHandler(this.chbFindGaps_CheckedChanged);
            // 
            // txbBwaResults
            // 
            this.txbBwaResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbBwaResults.Location = new System.Drawing.Point(8, 117);
            this.txbBwaResults.Multiline = true;
            this.txbBwaResults.Name = "txbBwaResults";
            this.txbBwaResults.ReadOnly = true;
            this.txbBwaResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbBwaResults.Size = new System.Drawing.Size(844, 374);
            this.txbBwaResults.TabIndex = 12;
            this.txbBwaResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBwaResults_KeyDown);
            // 
            // nupErrorsAllowed
            // 
            this.nupErrorsAllowed.Location = new System.Drawing.Point(181, 37);
            this.nupErrorsAllowed.Name = "nupErrorsAllowed";
            this.nupErrorsAllowed.Size = new System.Drawing.Size(85, 22);
            this.nupErrorsAllowed.TabIndex = 11;
            this.nupErrorsAllowed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblErrorsAllowed
            // 
            this.lblErrorsAllowed.AutoSize = true;
            this.lblErrorsAllowed.Location = new System.Drawing.Point(8, 39);
            this.lblErrorsAllowed.Name = "lblErrorsAllowed";
            this.lblErrorsAllowed.Size = new System.Drawing.Size(167, 17);
            this.lblErrorsAllowed.TabIndex = 9;
            this.lblErrorsAllowed.Text = "Number of errors allowed";
            // 
            // lblReference
            // 
            this.lblReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(271, 30);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(74, 17);
            this.lblReference.TabIndex = 10;
            this.lblReference.Text = "Reference";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(8, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(108, 17);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "String to search";
            // 
            // txbReference
            // 
            this.txbReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbReference.Location = new System.Drawing.Point(352, 30);
            this.txbReference.Multiline = true;
            this.txbReference.Name = "txbReference";
            this.txbReference.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbReference.Size = new System.Drawing.Size(500, 77);
            this.txbReference.TabIndex = 8;
            this.txbReference.Text = "google";
            this.txbReference.TextChanged += new System.EventHandler(this.txbReference_TextChanged);
            this.txbReference.DoubleClick += new System.EventHandler(this.txbReference_DoubleClick);
            this.txbReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbReference_KeyDown);
            // 
            // txbSearch
            // 
            this.txbSearch.Location = new System.Drawing.Point(181, 5);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(671, 22);
            this.txbSearch.TabIndex = 8;
            this.txbSearch.Text = "lol";
            this.txbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSearch_KeyPress);
            this.txbSearch.TextChanged += new System.EventHandler(this.txbSearch_TextChanged);
            // 
            // btnInexactSearch
            // 
            this.btnInexactSearch.Location = new System.Drawing.Point(181, 85);
            this.btnInexactSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnInexactSearch.Name = "btnInexactSearch";
            this.btnInexactSearch.Size = new System.Drawing.Size(164, 25);
            this.btnInexactSearch.TabIndex = 7;
            this.btnInexactSearch.Text = "Execute Inexact Search!";
            this.btnInexactSearch.UseVisualStyleBackColor = true;
            this.btnInexactSearch.Click += new System.EventHandler(this.btnInexactSearch_Click);
            // 
            // tpBwt
            // 
            this.tpBwt.Controls.Add(this.bchReverse);
            this.tpBwt.Controls.Add(this.bchBwt);
            this.tpBwt.Controls.Add(this.pbLed);
            this.tpBwt.Controls.Add(this.btnExecute);
            this.tpBwt.Controls.Add(this.lblSeparator);
            this.tpBwt.Controls.Add(this.lblReversedOutput);
            this.tpBwt.Controls.Add(this.lblOutPut);
            this.tpBwt.Controls.Add(this.lblInput);
            this.tpBwt.Controls.Add(this.txbReversedOutput);
            this.tpBwt.Controls.Add(this.txbOutPut);
            this.tpBwt.Controls.Add(this.txbInput);
            this.tpBwt.Location = new System.Drawing.Point(4, 25);
            this.tpBwt.Margin = new System.Windows.Forms.Padding(4);
            this.tpBwt.Name = "tpBwt";
            this.tpBwt.Padding = new System.Windows.Forms.Padding(4);
            this.tpBwt.Size = new System.Drawing.Size(860, 494);
            this.tpBwt.TabIndex = 0;
            this.tpBwt.Text = "BWT";
            this.tpBwt.UseVisualStyleBackColor = true;
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
            // pbLed
            // 
            this.pbLed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLed.ErrorImage = null;
            this.pbLed.Image = global::BWT.Properties.Resources.green_led;
            this.pbLed.Location = new System.Drawing.Point(793, 511);
            this.pbLed.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4);
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
            this.lblSeparator.Size = new System.Drawing.Size(838, 2);
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
            this.txbReversedOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txbReversedOutput.Multiline = true;
            this.txbReversedOutput.Name = "txbReversedOutput";
            this.txbReversedOutput.ReadOnly = true;
            this.txbReversedOutput.Size = new System.Drawing.Size(805, 158);
            this.txbReversedOutput.TabIndex = 0;
            // 
            // txbOutPut
            // 
            this.txbOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbOutPut.Location = new System.Drawing.Point(13, 334);
            this.txbOutPut.Margin = new System.Windows.Forms.Padding(4);
            this.txbOutPut.Multiline = true;
            this.txbOutPut.Name = "txbOutPut";
            this.txbOutPut.ReadOnly = true;
            this.txbOutPut.Size = new System.Drawing.Size(805, 158);
            this.txbOutPut.TabIndex = 0;
            // 
            // txbInput
            // 
            this.txbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbInput.Location = new System.Drawing.Point(13, 39);
            this.txbInput.Margin = new System.Windows.Forms.Padding(4);
            this.txbInput.Multiline = true;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(821, 171);
            this.txbInput.TabIndex = 0;
            this.txbInput.TextChanged += new System.EventHandler(this.txbInput_TextChanged);
            this.txbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbInput_KeyDown);
            // 
            // tpIntermediates
            // 
            this.tpIntermediates.Controls.Add(this.txbIntermediates);
            this.tpIntermediates.Location = new System.Drawing.Point(4, 25);
            this.tpIntermediates.Margin = new System.Windows.Forms.Padding(4);
            this.tpIntermediates.Name = "tpIntermediates";
            this.tpIntermediates.Padding = new System.Windows.Forms.Padding(4);
            this.tpIntermediates.Size = new System.Drawing.Size(860, 494);
            this.tpIntermediates.TabIndex = 2;
            this.tpIntermediates.Text = "Intermediate states";
            this.tpIntermediates.UseVisualStyleBackColor = true;
            // 
            // txbIntermediates
            // 
            this.txbIntermediates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbIntermediates.Location = new System.Drawing.Point(4, 4);
            this.txbIntermediates.Margin = new System.Windows.Forms.Padding(4);
            this.txbIntermediates.Multiline = true;
            this.txbIntermediates.Name = "txbIntermediates";
            this.txbIntermediates.ReadOnly = true;
            this.txbIntermediates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbIntermediates.Size = new System.Drawing.Size(852, 486);
            this.txbIntermediates.TabIndex = 0;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.lnkBwaPaper);
            this.tpSettings.Controls.Add(this.lnkWikipedia);
            this.tpSettings.Controls.Add(this.chbSpeedOverReports);
            this.tpSettings.Controls.Add(this.chbPerformReverseTransform);
            this.tpSettings.Location = new System.Drawing.Point(4, 25);
            this.tpSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tpSettings.Size = new System.Drawing.Size(860, 494);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // lnkBwaPaper
            // 
            this.lnkBwaPaper.AutoSize = true;
            this.lnkBwaPaper.Location = new System.Drawing.Point(8, 15);
            this.lnkBwaPaper.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkBwaPaper.Name = "lnkBwaPaper";
            this.lnkBwaPaper.Size = new System.Drawing.Size(81, 17);
            this.lnkBwaPaper.TabIndex = 1;
            this.lnkBwaPaper.TabStop = true;
            this.lnkBwaPaper.Text = "BWA Paper";
            this.lnkBwaPaper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWikipedia_LinkClicked);
            // 
            // lnkWikipedia
            // 
            this.lnkWikipedia.AutoSize = true;
            this.lnkWikipedia.Location = new System.Drawing.Point(8, 32);
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
            this.chbSpeedOverReports.Location = new System.Drawing.Point(11, 81);
            this.chbSpeedOverReports.Margin = new System.Windows.Forms.Padding(4);
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
            this.chbPerformReverseTransform.Location = new System.Drawing.Point(11, 53);
            this.chbPerformReverseTransform.Margin = new System.Windows.Forms.Padding(4);
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
            this.scMain.Margin = new System.Windows.Forms.Padding(4);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tcMain);
            this.scMain.Panel1MinSize = 500;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.pbTransform);
            this.scMain.Panel2MinSize = 10;
            this.scMain.Size = new System.Drawing.Size(868, 552);
            this.scMain.SplitterDistance = 523;
            this.scMain.SplitterWidth = 5;
            this.scMain.TabIndex = 1;
            // 
            // pbTransform
            // 
            this.pbTransform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTransform.Location = new System.Drawing.Point(0, 0);
            this.pbTransform.Margin = new System.Windows.Forms.Padding(4);
            this.pbTransform.Name = "pbTransform";
            this.pbTransform.Size = new System.Drawing.Size(868, 24);
            this.pbTransform.TabIndex = 0;
            // 
            // frmBwt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 552);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBwt";
            this.Text = "Burrows–Wheeler transform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBwt_FormClosing);
            this.tcMain.ResumeLayout(false);
            this.tpMultipleBwa.ResumeLayout(false);
            this.tpMultipleBwa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupNumberOfReads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupReadLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorPercentage)).EndInit();
            this.tpBwa.ResumeLayout(false);
            this.tpBwa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupErrorsAllowed)).EndInit();
            this.tpBwt.ResumeLayout(false);
            this.tpBwt.PerformLayout();
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

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpBwt;
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
        private System.Windows.Forms.TabPage tpBwa;
        private System.Windows.Forms.NumericUpDown nupErrorsAllowed;
        private System.Windows.Forms.Label lblErrorsAllowed;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Button btnInexactSearch;
        private System.Windows.Forms.TextBox txbBwaResults;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txbReference;
        private System.Windows.Forms.LinkLabel lnkBwaPaper;
        private System.Windows.Forms.CheckBox chbFindGaps;
        private System.Windows.Forms.TabPage tpMultipleBwa;
        private System.Windows.Forms.Label lblReferenceMirror;
        private System.Windows.Forms.TextBox txbReferenceMirror;
        private System.Windows.Forms.NumericUpDown nupErrorPercentage;
        private System.Windows.Forms.Label lblErrorPercentage;
        private System.Windows.Forms.NumericUpDown nupNumberOfReads;
        private System.Windows.Forms.Label lblNumberOfReads;
        private System.Windows.Forms.NumericUpDown nupReadLength;
        private System.Windows.Forms.Label lblReadLength;
        private System.Windows.Forms.Button btnStartMultipleBwa;
        private System.Windows.Forms.RadioButton rdvBwaBoth;
        private System.Windows.Forms.RadioButton rdvBwaMultipleThread;
        private System.Windows.Forms.RadioButton rdvBwaSingleThread;
        private System.Windows.Forms.TextBox txbMultiBwaResults;
        private System.Windows.Forms.Button btnClearMultiple;
        private System.Windows.Forms.Label lblRecommendedMaxError;


    }
}

