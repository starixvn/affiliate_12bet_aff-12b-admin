namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    partial class ImporterFormBase
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
            this.richTextConvertedData = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextRawData = new System.Windows.Forms.RichTextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCovert = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetRawData = new System.Windows.Forms.Button();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBoxConvertDirectFromTxtField = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextConvertedData
            // 
            this.richTextConvertedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextConvertedData.Location = new System.Drawing.Point(3, 16);
            this.richTextConvertedData.Name = "richTextConvertedData";
            this.richTextConvertedData.Size = new System.Drawing.Size(415, 381);
            this.richTextConvertedData.TabIndex = 0;
            this.richTextConvertedData.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextRawData);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 400);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Raw data";
            // 
            // richTextRawData
            // 
            this.richTextRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextRawData.Location = new System.Drawing.Point(3, 16);
            this.richTextRawData.Name = "richTextRawData";
            this.richTextRawData.Size = new System.Drawing.Size(397, 381);
            this.richTextRawData.TabIndex = 3;
            this.richTextRawData.Text = "";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(316, 58);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(132, 23);
            this.btnImport.TabIndex = 18;
            this.btnImport.Text = "Import Converted Data";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // endTimePicker
            // 
            this.endTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:00";
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTimePicker.Location = new System.Drawing.Point(375, 28);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(200, 20);
            this.endTimePicker.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "End Time:";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(687, 28);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new System.Drawing.Size(111, 20);
            this.txtPageNumber.TabIndex = 14;
            this.txtPageNumber.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(606, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Page Number:";
            // 
            // btnCovert
            // 
            this.btnCovert.Location = new System.Drawing.Point(149, 58);
            this.btnCovert.Name = "btnCovert";
            this.btnCovert.Size = new System.Drawing.Size(132, 23);
            this.btnCovert.TabIndex = 7;
            this.btnCovert.Text = "Converted Data";
            this.btnCovert.UseVisualStyleBackColor = true;
            this.btnCovert.Click += new System.EventHandler(this.btnCovert_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 213);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(829, 400);
            this.splitContainer1.SplitterDistance = 403;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextConvertedData);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(421, 400);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Converted Xml";
            // 
            // richTextLog
            // 
            this.richTextLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextLog.Location = new System.Drawing.Point(3, 16);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.Size = new System.Drawing.Size(823, 81);
            this.richTextLog.TabIndex = 0;
            this.richTextLog.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.richTextLog);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 113);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(829, 100);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Log";
            // 
            // btnGetRawData
            // 
            this.btnGetRawData.Location = new System.Drawing.Point(12, 58);
            this.btnGetRawData.Name = "btnGetRawData";
            this.btnGetRawData.Size = new System.Drawing.Size(106, 23);
            this.btnGetRawData.TabIndex = 6;
            this.btnGetRawData.Text = "Get Raw Data";
            this.btnGetRawData.UseVisualStyleBackColor = true;
            this.btnGetRawData.Click += new System.EventHandler(this.btnGetRawData_Click);
            // 
            // startTimePicker
            // 
            this.startTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:00";
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimePicker.Location = new System.Drawing.Point(79, 28);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxConvertDirectFromTxtField);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.endTimePicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPageNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnCovert);
            this.groupBox1.Controls.Add(this.btnGetRawData);
            this.groupBox1.Controls.Add(this.startTimePicker);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 113);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Param";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start Time:";
            // 
            // chkBoxConvertDirectFromTxtField
            // 
            this.chkBoxConvertDirectFromTxtField.AutoSize = true;
            this.chkBoxConvertDirectFromTxtField.Location = new System.Drawing.Point(15, 90);
            this.chkBoxConvertDirectFromTxtField.Name = "chkBoxConvertDirectFromTxtField";
            this.chkBoxConvertDirectFromTxtField.Size = new System.Drawing.Size(157, 17);
            this.chkBoxConvertDirectFromTxtField.TabIndex = 19;
            this.chkBoxConvertDirectFromTxtField.Text = "Convert direct from text field";
            this.chkBoxConvertDirectFromTxtField.UseVisualStyleBackColor = true;
            // 
            // ImporterFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 613);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImporterFormBase";
            this.Text = "ImporterFormBase";
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextConvertedData;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.RichTextBox richTextRawData;
        public System.Windows.Forms.Button btnImport;
        public System.Windows.Forms.DateTimePicker endTimePicker;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPageNumber;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnCovert;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.RichTextBox richTextLog;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Button btnGetRawData;
        public System.Windows.Forms.DateTimePicker startTimePicker;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxConvertDirectFromTxtField;
    }
}