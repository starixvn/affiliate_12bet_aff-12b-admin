namespace Fanex.Affiliate.DataService.WinFormApp
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCasino = new System.Windows.Forms.Button();
            this.btnBetsoft = new System.Windows.Forms.Button();
            this.btnEbet = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select service that you want to run:";
            // 
            // btnCasino
            // 
            this.btnCasino.Location = new System.Drawing.Point(15, 66);
            this.btnCasino.Name = "btnCasino";
            this.btnCasino.Size = new System.Drawing.Size(110, 23);
            this.btnCasino.TabIndex = 1;
            this.btnCasino.Text = "Casino";
            this.btnCasino.UseVisualStyleBackColor = true;
            this.btnCasino.Click += new System.EventHandler(this.btnCasino_Click);
            // 
            // btnBetsoft
            // 
            this.btnBetsoft.Location = new System.Drawing.Point(153, 66);
            this.btnBetsoft.Name = "btnBetsoft";
            this.btnBetsoft.Size = new System.Drawing.Size(110, 23);
            this.btnBetsoft.TabIndex = 2;
            this.btnBetsoft.Text = "12Amber - Betsoft";
            this.btnBetsoft.UseVisualStyleBackColor = true;
            this.btnBetsoft.Click += new System.EventHandler(this.btnBetsoft_Click);
            // 
            // btnEbet
            // 
            this.btnEbet.Location = new System.Drawing.Point(15, 112);
            this.btnEbet.Name = "btnEbet";
            this.btnEbet.Size = new System.Drawing.Size(110, 23);
            this.btnEbet.TabIndex = 3;
            this.btnEbet.Text = "12Opal - EBet";
            this.btnEbet.UseVisualStyleBackColor = true;
            this.btnEbet.Click += new System.EventHandler(this.btnEbet_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "12Crystal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEbet);
            this.Controls.Add(this.btnBetsoft);
            this.Controls.Add(this.btnCasino);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCasino;
        private System.Windows.Forms.Button btnBetsoft;
        private System.Windows.Forms.Button btnEbet;
        private System.Windows.Forms.Button button1;
    }
}