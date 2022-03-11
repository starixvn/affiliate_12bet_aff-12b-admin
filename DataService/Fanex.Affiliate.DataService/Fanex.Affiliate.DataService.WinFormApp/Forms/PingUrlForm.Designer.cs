
namespace Fanex.Affiliate.DataService.WinFormApp.Forms
{
    partial class PingUrlForm
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
            this.lblUrlPath = new System.Windows.Forms.Label();
            this.txtUrlPath = new System.Windows.Forms.TextBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.radioBtnMethodGet = new System.Windows.Forms.RadioButton();
            this.radioBtnMethodPost = new System.Windows.Forms.RadioButton();
            this.lblPingResult = new System.Windows.Forms.Label();
            this.btnPing = new System.Windows.Forms.Button();
            this.richTbxPingResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblUrlPath
            // 
            this.lblUrlPath.AutoSize = true;
            this.lblUrlPath.Location = new System.Drawing.Point(40, 38);
            this.lblUrlPath.Name = "lblUrlPath";
            this.lblUrlPath.Size = new System.Drawing.Size(63, 17);
            this.lblUrlPath.TabIndex = 0;
            this.lblUrlPath.Text = "Url Path:";
            // 
            // txtUrlPath
            // 
            this.txtUrlPath.Location = new System.Drawing.Point(138, 38);
            this.txtUrlPath.Name = "txtUrlPath";
            this.txtUrlPath.Size = new System.Drawing.Size(477, 22);
            this.txtUrlPath.TabIndex = 1;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(40, 85);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(59, 17);
            this.lblMethod.TabIndex = 2;
            this.lblMethod.Text = "Method:";
            // 
            // radioBtnMethodGet
            // 
            this.radioBtnMethodGet.AutoSize = true;
            this.radioBtnMethodGet.Location = new System.Drawing.Point(138, 85);
            this.radioBtnMethodGet.Name = "radioBtnMethodGet";
            this.radioBtnMethodGet.Size = new System.Drawing.Size(58, 21);
            this.radioBtnMethodGet.TabIndex = 3;
            this.radioBtnMethodGet.TabStop = true;
            this.radioBtnMethodGet.Text = "GET";
            this.radioBtnMethodGet.UseVisualStyleBackColor = true;
            // 
            // radioBtnMethodPost
            // 
            this.radioBtnMethodPost.AutoSize = true;
            this.radioBtnMethodPost.Location = new System.Drawing.Point(226, 85);
            this.radioBtnMethodPost.Name = "radioBtnMethodPost";
            this.radioBtnMethodPost.Size = new System.Drawing.Size(67, 21);
            this.radioBtnMethodPost.TabIndex = 4;
            this.radioBtnMethodPost.TabStop = true;
            this.radioBtnMethodPost.Text = "POST";
            this.radioBtnMethodPost.UseVisualStyleBackColor = true;
            // 
            // lblPingResult
            // 
            this.lblPingResult.AutoSize = true;
            this.lblPingResult.Location = new System.Drawing.Point(40, 140);
            this.lblPingResult.Name = "lblPingResult";
            this.lblPingResult.Size = new System.Drawing.Size(80, 17);
            this.lblPingResult.TabIndex = 5;
            this.lblPingResult.Text = "Ping Result";
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(639, 34);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(104, 31);
            this.btnPing.TabIndex = 6;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // richTbxPingResult
            // 
            this.richTbxPingResult.Location = new System.Drawing.Point(25, 164);
            this.richTbxPingResult.Name = "richTbxPingResult";
            this.richTbxPingResult.Size = new System.Drawing.Size(749, 261);
            this.richTbxPingResult.TabIndex = 7;
            this.richTbxPingResult.Text = "";
            // 
            // PingUrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTbxPingResult);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.lblPingResult);
            this.Controls.Add(this.radioBtnMethodPost);
            this.Controls.Add(this.radioBtnMethodGet);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.txtUrlPath);
            this.Controls.Add(this.lblUrlPath);
            this.Name = "PingUrlForm";
            this.Text = "Ping Url";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrlPath;
        private System.Windows.Forms.TextBox txtUrlPath;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.RadioButton radioBtnMethodGet;
        private System.Windows.Forms.RadioButton radioBtnMethodPost;
        private System.Windows.Forms.Label lblPingResult;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.RichTextBox richTbxPingResult;
    }
}