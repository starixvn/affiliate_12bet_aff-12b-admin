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
            this.btnImportDirectly = new System.Windows.Forms.Button();
            this.lbServiceTime = new System.Windows.Forms.Label();
            this.lbLocalTime = new System.Windows.Forms.Label();
            this.datePickerLocalEndTime = new System.Windows.Forms.DateTimePicker();
            this.datePickerLocalStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBoxConvertDirectFromTxtField = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.richTextConvertedData.Size = new System.Drawing.Size(415, 329);
            this.richTextConvertedData.TabIndex = 0;
            this.richTextConvertedData.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextRawData);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 348);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Raw data";
            // 
            // richTextRawData
            // 
            this.richTextRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextRawData.Location = new System.Drawing.Point(3, 16);
            this.richTextRawData.Name = "richTextRawData";
            this.richTextRawData.Size = new System.Drawing.Size(397, 329);
            this.richTextRawData.TabIndex = 3;
            this.richTextRawData.Text = "";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(316, 105);
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
            this.endTimePicker.Location = new System.Drawing.Point(424, 40);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.Size = new System.Drawing.Size(200, 20);
            this.endTimePicker.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Server End Time:";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(710, 40);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new System.Drawing.Size(111, 20);
            this.txtPageNumber.TabIndex = 14;
            this.txtPageNumber.Text = "1";
            this.txtPageNumber.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(629, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Page Number:";
            this.label4.Visible = false;
            // 
            // btnCovert
            // 
            this.btnCovert.Location = new System.Drawing.Point(149, 105);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 265);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(829, 348);
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
            this.groupBox4.Size = new System.Drawing.Size(421, 348);
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
            this.groupBox5.Location = new System.Drawing.Point(0, 165);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(829, 100);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Log";
            // 
            // btnGetRawData
            // 
            this.btnGetRawData.Location = new System.Drawing.Point(12, 105);
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
            this.startTimePicker.Location = new System.Drawing.Point(109, 40);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startTimePicker.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImportDirectly);
            this.groupBox1.Controls.Add(this.lbServiceTime);
            this.groupBox1.Controls.Add(this.lbLocalTime);
            this.groupBox1.Controls.Add(this.datePickerLocalEndTime);
            this.groupBox1.Controls.Add(this.datePickerLocalStartTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
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
            this.groupBox1.Size = new System.Drawing.Size(829, 165);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Param";
            // 
            // btnImportDirectly
            // 
            this.btnImportDirectly.Location = new System.Drawing.Point(470, 105);
            this.btnImportDirectly.Name = "btnImportDirectly";
            this.btnImportDirectly.Size = new System.Drawing.Size(132, 23);
            this.btnImportDirectly.TabIndex = 26;
            this.btnImportDirectly.Text = "Import Directly";
            this.btnImportDirectly.UseVisualStyleBackColor = true;
            this.btnImportDirectly.Click += new System.EventHandler(this.btnImportDirectly_Click);
            // 
            // lbServiceTime
            // 
            this.lbServiceTime.AutoSize = true;
            this.lbServiceTime.Location = new System.Drawing.Point(186, 16);
            this.lbServiceTime.Name = "lbServiceTime";
            this.lbServiceTime.Size = new System.Drawing.Size(109, 13);
            this.lbServiceTime.TabIndex = 25;
            this.lbServiceTime.Text = "Server Time: GMT +8";
            // 
            // lbLocalTime
            // 
            this.lbLocalTime.AutoSize = true;
            this.lbLocalTime.Location = new System.Drawing.Point(12, 16);
            this.lbLocalTime.Name = "lbLocalTime";
            this.lbLocalTime.Size = new System.Drawing.Size(104, 13);
            this.lbLocalTime.TabIndex = 24;
            this.lbLocalTime.Text = "Local Time: GMT +7";
            // 
            // datePickerLocalEndTime
            // 
            this.datePickerLocalEndTime.CustomFormat = "yyyy/MM/dd HH:mm:00";
            this.datePickerLocalEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerLocalEndTime.Location = new System.Drawing.Point(443, 72);
            this.datePickerLocalEndTime.Name = "datePickerLocalEndTime";
            this.datePickerLocalEndTime.Size = new System.Drawing.Size(181, 20);
            this.datePickerLocalEndTime.TabIndex = 23;
            this.datePickerLocalEndTime.ValueChanged += new System.EventHandler(this.datePickerLocalEndTime_ValueChanged);
            // 
            // datePickerLocalStartTime
            // 
            this.datePickerLocalStartTime.CustomFormat = "yyyy/MM/dd HH:mm:00";
            this.datePickerLocalStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerLocalStartTime.Location = new System.Drawing.Point(118, 72);
            this.datePickerLocalStartTime.Name = "datePickerLocalStartTime";
            this.datePickerLocalStartTime.Size = new System.Drawing.Size(200, 20);
            this.datePickerLocalStartTime.TabIndex = 22;
            this.datePickerLocalStartTime.ValueChanged += new System.EventHandler(this.datePickerLocalStartTime_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Local End Time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Local Start Time:";
            // 
            // chkBoxConvertDirectFromTxtField
            // 
            this.chkBoxConvertDirectFromTxtField.AutoSize = true;
            this.chkBoxConvertDirectFromTxtField.Location = new System.Drawing.Point(15, 138);
            this.chkBoxConvertDirectFromTxtField.Name = "chkBoxConvertDirectFromTxtField";
            this.chkBoxConvertDirectFromTxtField.Size = new System.Drawing.Size(157, 17);
            this.chkBoxConvertDirectFromTxtField.TabIndex = 19;
            this.chkBoxConvertDirectFromTxtField.Text = "Convert direct from text field";
            this.chkBoxConvertDirectFromTxtField.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server Start Time:";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImporterFormBase_FormClosing);
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
        public System.Windows.Forms.Label lbServiceTime;
        public System.Windows.Forms.Label lbLocalTime;
        public System.Windows.Forms.DateTimePicker datePickerLocalEndTime;
        public System.Windows.Forms.DateTimePicker datePickerLocalStartTime;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnImportDirectly;
    }
}