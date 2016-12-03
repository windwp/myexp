namespace autokey
{
    partial class AutoFormControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoFormControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.form1_cbx = new System.Windows.Forms.CheckBox();
            this.form1TimeMouseClick_tbx = new System.Windows.Forms.TextBox();
            this.form1TimeWait_tbx = new System.Windows.Forms.TextBox();
            this.form1TimeRun_tbx = new System.Windows.Forms.TextBox();
            this.form1MousePos_tbx = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textForm1_tbx = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameForm1_tbx = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.form1_cbx);
            this.panel1.Controls.Add(this.form1TimeMouseClick_tbx);
            this.panel1.Controls.Add(this.form1TimeWait_tbx);
            this.panel1.Controls.Add(this.form1TimeRun_tbx);
            this.panel1.Controls.Add(this.form1MousePos_tbx);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.textForm1_tbx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nameForm1_tbx);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 545);
            this.panel1.TabIndex = 1;
            // 
            // form1_cbx
            // 
            this.form1_cbx.AutoSize = true;
            this.form1_cbx.Location = new System.Drawing.Point(106, 71);
            this.form1_cbx.Name = "form1_cbx";
            this.form1_cbx.Size = new System.Drawing.Size(179, 17);
            this.form1_cbx.TabIndex = 6;
            this.form1_cbx.Text = "send back key after mouse click";
            this.form1_cbx.UseVisualStyleBackColor = true;
            // 
            // form1TimeMouseClick_tbx
            // 
            this.form1TimeMouseClick_tbx.Location = new System.Drawing.Point(273, 169);
            this.form1TimeMouseClick_tbx.Name = "form1TimeMouseClick_tbx";
            this.form1TimeMouseClick_tbx.Size = new System.Drawing.Size(67, 20);
            this.form1TimeMouseClick_tbx.TabIndex = 5;
            this.form1TimeMouseClick_tbx.Text = "5";
            // 
            // form1TimeWait_tbx
            // 
            this.form1TimeWait_tbx.Location = new System.Drawing.Point(273, 143);
            this.form1TimeWait_tbx.Name = "form1TimeWait_tbx";
            this.form1TimeWait_tbx.Size = new System.Drawing.Size(67, 20);
            this.form1TimeWait_tbx.TabIndex = 5;
            this.form1TimeWait_tbx.Text = "6";
            // 
            // form1TimeRun_tbx
            // 
            this.form1TimeRun_tbx.Location = new System.Drawing.Point(273, 116);
            this.form1TimeRun_tbx.Name = "form1TimeRun_tbx";
            this.form1TimeRun_tbx.Size = new System.Drawing.Size(67, 20);
            this.form1TimeRun_tbx.TabIndex = 5;
            this.form1TimeRun_tbx.Text = "1";
            // 
            // form1MousePos_tbx
            // 
            this.form1MousePos_tbx.Location = new System.Drawing.Point(106, 90);
            this.form1MousePos_tbx.Name = "form1MousePos_tbx";
            this.form1MousePos_tbx.Size = new System.Drawing.Size(237, 20);
            this.form1MousePos_tbx.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(103, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(237, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // textForm1_tbx
            // 
            this.textForm1_tbx.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textForm1_tbx.Location = new System.Drawing.Point(0, 226);
            this.textForm1_tbx.Name = "textForm1_tbx";
            this.textForm1_tbx.Size = new System.Drawing.Size(358, 319);
            this.textForm1_tbx.TabIndex = 3;
            this.textForm1_tbx.Text = resources.GetString("textForm1_tbx.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Form1TextKey";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(169, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "TimeMouseClick(s)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "TimeWait(second)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(169, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "TimeRun(second)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Form1MousePos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Form1Name";
            // 
            // nameForm1_tbx
            // 
            this.nameForm1_tbx.Location = new System.Drawing.Point(103, 11);
            this.nameForm1_tbx.Name = "nameForm1_tbx";
            this.nameForm1_tbx.Size = new System.Drawing.Size(237, 20);
            this.nameForm1_tbx.TabIndex = 0;
            this.nameForm1_tbx.Text = "Sublime Text";
            // 
            // AutoFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "AutoFormControl";
            this.Size = new System.Drawing.Size(364, 551);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox form1_cbx;
        private System.Windows.Forms.TextBox form1TimeMouseClick_tbx;
        private System.Windows.Forms.TextBox form1TimeWait_tbx;
        private System.Windows.Forms.TextBox form1TimeRun_tbx;
        private System.Windows.Forms.TextBox form1MousePos_tbx;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RichTextBox textForm1_tbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameForm1_tbx;
    }
}
