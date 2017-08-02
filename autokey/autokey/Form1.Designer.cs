namespace autokey
{
    partial class Form1
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
            this.start_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stop_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.totalTime_lb = new System.Windows.Forms.Label();
            this.numberForm_tbx = new System.Windows.Forms.TextBox();
            this.totalTime_tbx = new System.Windows.Forms.TextBox();
            this.log_rbtx = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.key_back_tbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(0, 19);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(75, 23);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.key_back_tbx);
            this.groupBox1.Controls.Add(this.start_btn);
            this.groupBox1.Controls.Add(this.stop_btn);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.totalTime_lb);
            this.groupBox1.Controls.Add(this.numberForm_tbx);
            this.groupBox1.Controls.Add(this.totalTime_tbx);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(81, 19);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(75, 23);
            this.stop_btn.TabIndex = 1;
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Number Form";
            // 
            // totalTime_lb
            // 
            this.totalTime_lb.AutoSize = true;
            this.totalTime_lb.Location = new System.Drawing.Point(175, 21);
            this.totalTime_lb.Name = "totalTime_lb";
            this.totalTime_lb.Size = new System.Drawing.Size(99, 13);
            this.totalTime_lb.TabIndex = 2;
            this.totalTime_lb.Text = "TotalTime (minutes)";
            // 
            // numberForm_tbx
            // 
            this.numberForm_tbx.Location = new System.Drawing.Point(81, 62);
            this.numberForm_tbx.Name = "numberForm_tbx";
            this.numberForm_tbx.Size = new System.Drawing.Size(100, 20);
            this.numberForm_tbx.TabIndex = 6;
            this.numberForm_tbx.Text = "3";
            this.numberForm_tbx.TextChanged += new System.EventHandler(this.numberForm_tbx_TextChanged);
            // 
            // totalTime_tbx
            // 
            this.totalTime_tbx.Location = new System.Drawing.Point(276, 18);
            this.totalTime_tbx.Name = "totalTime_tbx";
            this.totalTime_tbx.Size = new System.Drawing.Size(67, 20);
            this.totalTime_tbx.TabIndex = 5;
            this.totalTime_tbx.Text = "120";
            // 
            // log_rbtx
            // 
            this.log_rbtx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_rbtx.Location = new System.Drawing.Point(379, 18);
            this.log_rbtx.Name = "log_rbtx";
            this.log_rbtx.Size = new System.Drawing.Size(424, 111);
            this.log_rbtx.TabIndex = 8;
            this.log_rbtx.Text = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 147);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1227, 579);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.log_rbtx);
            this.groupBox2.Location = new System.Drawing.Point(418, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(809, 129);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Help";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "*F3 for stop running\r\n*F10 for choose mouse position\r\n  (focus on FormMousePos fi" +
    "rst then press F10)\r\n";
            // 
            // key_back_tbx
            // 
            this.key_back_tbx.Location = new System.Drawing.Point(217, 91);
            this.key_back_tbx.Name = "key_back_tbx";
            this.key_back_tbx.Size = new System.Drawing.Size(126, 20);
            this.key_back_tbx.TabIndex = 8;
            this.key_back_tbx.Text = "%{SUBTRACT}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Key back to previous location in editor";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 726);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.TextBox totalTime_tbx;
        private System.Windows.Forms.Label totalTime_lb;
        private System.Windows.Forms.TextBox numberForm_tbx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox log_rbtx;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox key_back_tbx;
        private System.Windows.Forms.Label label2;
    }
}

