namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    partial class FileImportForm
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
            this.cbProductType = new System.Windows.Forms.ComboBox();
            this.lbProductType = new System.Windows.Forms.Label();
            this.txtBoxFilePath = new System.Windows.Forms.TextBox();
            this.richTxtBoxConvertedXml = new System.Windows.Forms.RichTextBox();
            this.btnChoseFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cbProductType
            // 
            this.cbProductType.FormattingEnabled = true;
            this.cbProductType.Location = new System.Drawing.Point(113, 10);
            this.cbProductType.Name = "cbProductType";
            this.cbProductType.Size = new System.Drawing.Size(168, 21);
            this.cbProductType.TabIndex = 0;
            this.cbProductType.SelectedIndexChanged += new System.EventHandler(this.cbProductType_SelectedIndexChanged);
            // 
            // lbProductType
            // 
            this.lbProductType.AutoSize = true;
            this.lbProductType.Location = new System.Drawing.Point(12, 13);
            this.lbProductType.Name = "lbProductType";
            this.lbProductType.Size = new System.Drawing.Size(71, 13);
            this.lbProductType.TabIndex = 1;
            this.lbProductType.Text = "Product Type";
            // 
            // txtBoxFilePath
            // 
            this.txtBoxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxFilePath.Location = new System.Drawing.Point(113, 50);
            this.txtBoxFilePath.Name = "txtBoxFilePath";
            this.txtBoxFilePath.Size = new System.Drawing.Size(296, 20);
            this.txtBoxFilePath.TabIndex = 2;
            // 
            // richTxtBoxConvertedXml
            // 
            this.richTxtBoxConvertedXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTxtBoxConvertedXml.Location = new System.Drawing.Point(15, 144);
            this.richTxtBoxConvertedXml.Name = "richTxtBoxConvertedXml";
            this.richTxtBoxConvertedXml.Size = new System.Drawing.Size(449, 305);
            this.richTxtBoxConvertedXml.TabIndex = 3;
            this.richTxtBoxConvertedXml.Text = "";
            // 
            // btnChoseFile
            // 
            this.btnChoseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoseFile.Location = new System.Drawing.Point(415, 50);
            this.btnChoseFile.Name = "btnChoseFile";
            this.btnChoseFile.Size = new System.Drawing.Size(30, 23);
            this.btnChoseFile.TabIndex = 4;
            this.btnChoseFile.Text = "...";
            this.btnChoseFile.UseVisualStyleBackColor = true;
            this.btnChoseFile.Click += new System.EventHandler(this.btnChoseFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "File Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "XML Format";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(15, 90);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(103, 23);
            this.btnConvert.TabIndex = 7;
            this.btnConvert.Text = "Convert To XML";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(140, 90);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(103, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // FileImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 461);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChoseFile);
            this.Controls.Add(this.richTxtBoxConvertedXml);
            this.Controls.Add(this.txtBoxFilePath);
            this.Controls.Add(this.lbProductType);
            this.Controls.Add(this.cbProductType);
            this.Name = "FileImport";
            this.Text = "FileImport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbProductType;
        private System.Windows.Forms.Label lbProductType;
        private System.Windows.Forms.TextBox txtBoxFilePath;
        private System.Windows.Forms.RichTextBox richTxtBoxConvertedXml;
        private System.Windows.Forms.Button btnChoseFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}