namespace osu__Tool
{
    partial class Form
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.songsGridView = new System.Windows.Forms.DataGridView();
            this.artistColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.difficultyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.htCheckBox = new System.Windows.Forms.CheckBox();
            this.hrCheckBox = new System.Windows.Forms.CheckBox();
            this.dtCheckBox = new System.Windows.Forms.CheckBox();
            this.ezCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.beatmapLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.singleTapMaxBPMNumeric = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tappingStyleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timingOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.randomizeKeyTimingsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hitScanPixelOffsetNumeric = new System.Windows.Forms.NumericUpDown();
            this.hitScanCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.songsGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.singleTapMaxBPMNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timingOffsetNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hitScanPixelOffsetNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(464, 281);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.songsGridView);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Controls.Add(this.searchTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(456, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Beatmap";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // songsGridView
            // 
            this.songsGridView.AllowUserToAddRows = false;
            this.songsGridView.AllowUserToDeleteRows = false;
            this.songsGridView.AllowUserToOrderColumns = true;
            this.songsGridView.AllowUserToResizeRows = false;
            this.songsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.songsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.songsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.artistColumn,
            this.nameColumn,
            this.creatorColumn,
            this.difficultyColumn});
            this.songsGridView.GridColor = System.Drawing.SystemColors.Control;
            this.songsGridView.Location = new System.Drawing.Point(6, 32);
            this.songsGridView.MultiSelect = false;
            this.songsGridView.Name = "songsGridView";
            this.songsGridView.ReadOnly = true;
            this.songsGridView.RowHeadersVisible = false;
            this.songsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.songsGridView.Size = new System.Drawing.Size(339, 195);
            this.songsGridView.TabIndex = 5;
            this.songsGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SongsGridView_CellDoubleClick);
            // 
            // artistColumn
            // 
            this.artistColumn.HeaderText = "Artist";
            this.artistColumn.Name = "artistColumn";
            this.artistColumn.ReadOnly = true;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // creatorColumn
            // 
            this.creatorColumn.HeaderText = "Creator";
            this.creatorColumn.Name = "creatorColumn";
            this.creatorColumn.ReadOnly = true;
            // 
            // difficultyColumn
            // 
            this.difficultyColumn.HeaderText = "Difficulty";
            this.difficultyColumn.Name = "difficultyColumn";
            this.difficultyColumn.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.htCheckBox);
            this.groupBox1.Controls.Add(this.hrCheckBox);
            this.groupBox1.Controls.Add(this.dtCheckBox);
            this.groupBox1.Controls.Add(this.ezCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(351, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 111);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mods";
            // 
            // htCheckBox
            // 
            this.htCheckBox.AutoSize = true;
            this.htCheckBox.Location = new System.Drawing.Point(6, 88);
            this.htCheckBox.Name = "htCheckBox";
            this.htCheckBox.Size = new System.Drawing.Size(71, 17);
            this.htCheckBox.TabIndex = 7;
            this.htCheckBox.Text = "Half Time";
            this.htCheckBox.UseVisualStyleBackColor = true;
            this.htCheckBox.CheckedChanged += new System.EventHandler(this.HtCheckBox_CheckedChanged);
            // 
            // hrCheckBox
            // 
            this.hrCheckBox.AutoSize = true;
            this.hrCheckBox.Location = new System.Drawing.Point(6, 42);
            this.hrCheckBox.Name = "hrCheckBox";
            this.hrCheckBox.Size = new System.Drawing.Size(78, 17);
            this.hrCheckBox.TabIndex = 1;
            this.hrCheckBox.Text = "Hard Rock";
            this.hrCheckBox.UseVisualStyleBackColor = true;
            this.hrCheckBox.CheckedChanged += new System.EventHandler(this.HrCheckBox_CheckedChanged);
            // 
            // dtCheckBox
            // 
            this.dtCheckBox.AutoSize = true;
            this.dtCheckBox.Location = new System.Drawing.Point(6, 65);
            this.dtCheckBox.Name = "dtCheckBox";
            this.dtCheckBox.Size = new System.Drawing.Size(86, 17);
            this.dtCheckBox.TabIndex = 6;
            this.dtCheckBox.Text = "Double Time";
            this.dtCheckBox.UseVisualStyleBackColor = true;
            this.dtCheckBox.CheckedChanged += new System.EventHandler(this.DtCheckBox_CheckedChanged);
            // 
            // ezCheckBox
            // 
            this.ezCheckBox.AutoSize = true;
            this.ezCheckBox.Location = new System.Drawing.Point(6, 19);
            this.ezCheckBox.Name = "ezCheckBox";
            this.ezCheckBox.Size = new System.Drawing.Size(49, 17);
            this.ezCheckBox.TabIndex = 0;
            this.ezCheckBox.Text = "Easy";
            this.ezCheckBox.UseVisualStyleBackColor = true;
            this.ezCheckBox.CheckedChanged += new System.EventHandler(this.EzCheckBox_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.beatmapLabel});
            this.statusStrip1.Location = new System.Drawing.Point(3, 230);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(450, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel1.Text = "Beatmap:";
            // 
            // beatmapLabel
            // 
            this.beatmapLabel.Name = "beatmapLabel";
            this.beatmapLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Location = new System.Drawing.Point(6, 6);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(444, 20);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.singleTapMaxBPMNumeric);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tappingStyleComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.timingOffsetNumeric);
            this.groupBox2.Controls.Add(this.randomizeKeyTimingsCheckBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.hitScanPixelOffsetNumeric);
            this.groupBox2.Controls.Add(this.hitScanCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 177);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relax";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Single Tap Max BPM";
            // 
            // singleTapMaxBPMNumeric
            // 
            this.singleTapMaxBPMNumeric.Location = new System.Drawing.Point(6, 145);
            this.singleTapMaxBPMNumeric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.singleTapMaxBPMNumeric.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.singleTapMaxBPMNumeric.Name = "singleTapMaxBPMNumeric";
            this.singleTapMaxBPMNumeric.Size = new System.Drawing.Size(39, 20);
            this.singleTapMaxBPMNumeric.TabIndex = 12;
            this.singleTapMaxBPMNumeric.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.singleTapMaxBPMNumeric.ValueChanged += new System.EventHandler(this.SingleTapMaxBPMNumeric_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tapping Style";
            // 
            // tappingStyleComboBox
            // 
            this.tappingStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tappingStyleComboBox.FormattingEnabled = true;
            this.tappingStyleComboBox.Items.AddRange(new object[] {
            "Alternate",
            "Single"});
            this.tappingStyleComboBox.Location = new System.Drawing.Point(7, 118);
            this.tappingStyleComboBox.Name = "tappingStyleComboBox";
            this.tappingStyleComboBox.Size = new System.Drawing.Size(81, 21);
            this.tappingStyleComboBox.TabIndex = 10;
            this.tappingStyleComboBox.SelectedIndexChanged += new System.EventHandler(this.TappingStyleComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Timing Offset";
            // 
            // timingOffsetNumeric
            // 
            this.timingOffsetNumeric.Location = new System.Drawing.Point(6, 68);
            this.timingOffsetNumeric.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.timingOffsetNumeric.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.timingOffsetNumeric.Name = "timingOffsetNumeric";
            this.timingOffsetNumeric.Size = new System.Drawing.Size(38, 20);
            this.timingOffsetNumeric.TabIndex = 8;
            this.timingOffsetNumeric.Value = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.timingOffsetNumeric.ValueChanged += new System.EventHandler(this.TimingOffsetNumeric_ValueChanged);
            // 
            // randomizeKeyTimingsCheckBox
            // 
            this.randomizeKeyTimingsCheckBox.AutoSize = true;
            this.randomizeKeyTimingsCheckBox.Location = new System.Drawing.Point(6, 94);
            this.randomizeKeyTimingsCheckBox.Name = "randomizeKeyTimingsCheckBox";
            this.randomizeKeyTimingsCheckBox.Size = new System.Drawing.Size(139, 17);
            this.randomizeKeyTimingsCheckBox.TabIndex = 7;
            this.randomizeKeyTimingsCheckBox.Text = "Randomize Key Timings";
            this.randomizeKeyTimingsCheckBox.UseVisualStyleBackColor = true;
            this.randomizeKeyTimingsCheckBox.CheckedChanged += new System.EventHandler(this.RandomizeKeyHoldTime_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hit Scan Pixel Offset";
            // 
            // hitScanPixelOffsetNumeric
            // 
            this.hitScanPixelOffsetNumeric.Location = new System.Drawing.Point(6, 42);
            this.hitScanPixelOffsetNumeric.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.hitScanPixelOffsetNumeric.Name = "hitScanPixelOffsetNumeric";
            this.hitScanPixelOffsetNumeric.Size = new System.Drawing.Size(34, 20);
            this.hitScanPixelOffsetNumeric.TabIndex = 5;
            this.hitScanPixelOffsetNumeric.ValueChanged += new System.EventHandler(this.HitScanPixelOffsetNumeric_ValueChanged);
            // 
            // hitScanCheckBox
            // 
            this.hitScanCheckBox.AutoSize = true;
            this.hitScanCheckBox.Checked = true;
            this.hitScanCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hitScanCheckBox.Location = new System.Drawing.Point(6, 19);
            this.hitScanCheckBox.Name = "hitScanCheckBox";
            this.hitScanCheckBox.Size = new System.Drawing.Size(67, 17);
            this.hitScanCheckBox.TabIndex = 4;
            this.hitScanCheckBox.Text = "Hit Scan";
            this.hitScanCheckBox.UseVisualStyleBackColor = true;
            this.hitScanCheckBox.CheckedChanged += new System.EventHandler(this.HitScanCheckBox_CheckedChanged);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "Form";
            this.Text = "osu! Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.songsGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.singleTapMaxBPMNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timingOffsetNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hitScanPixelOffsetNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel beatmapLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox hitScanCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown hitScanPixelOffsetNumeric;
        private System.Windows.Forms.CheckBox randomizeKeyTimingsCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown timingOffsetNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tappingStyleComboBox;
        private System.Windows.Forms.NumericUpDown singleTapMaxBPMNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox htCheckBox;
        private System.Windows.Forms.CheckBox hrCheckBox;
        private System.Windows.Forms.CheckBox dtCheckBox;
        private System.Windows.Forms.CheckBox ezCheckBox;
        private System.Windows.Forms.DataGridView songsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn artistColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creatorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn difficultyColumn;
    }
}

