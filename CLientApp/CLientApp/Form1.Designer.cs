namespace CLientApp
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnStartWork = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lstItems);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 507);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtHost);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Location = new System.Drawing.Point(9, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Host Url:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(6, 46);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(350, 27);
            this.txtHost.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(362, 46);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 27);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPing);
            this.groupBox1.Controls.Add(this.btnStartWork);
            this.groupBox1.Location = new System.Drawing.Point(9, 443);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 61);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(231, 17);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(131, 30);
            this.btnPing.TabIndex = 1;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // btnStartWork
            // 
            this.btnStartWork.Location = new System.Drawing.Point(368, 17);
            this.btnStartWork.Name = "btnStartWork";
            this.btnStartWork.Size = new System.Drawing.Size(125, 30);
            this.btnStartWork.TabIndex = 2;
            this.btnStartWork.Text = "Start work";
            this.btnStartWork.UseVisualStyleBackColor = true;
            this.btnStartWork.Click += new System.EventHandler(this.btnStartWork_Click);
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 20;
            this.lstItems.Location = new System.Drawing.Point(9, 86);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(496, 364);
            this.lstItems.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 531);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private Button btnPing;
        private Button btnStartWork;
        private ListBox lstItems;
        private GroupBox groupBox2;
        private Label label1;
        private TextBox txtHost;
        private Button btnConnect;
    }
}