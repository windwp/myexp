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
            this.panel1 = new System.Windows.Forms.Panel();
            this.focus_btn = new System.Windows.Forms.Button();
            this.isEnable = new System.Windows.Forms.CheckBox();
            this.form1_cbx = new System.Windows.Forms.CheckBox();
            this.timeMouseClick_tbx = new System.Windows.Forms.TextBox();
            this.timeRun_tbx = new System.Windows.Forms.TextBox();
            this.mousePos_tbx = new System.Windows.Forms.TextBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.text_tbx = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameForm_tbx = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.focus_btn);
            this.panel1.Controls.Add(this.isEnable);
            this.panel1.Controls.Add(this.form1_cbx);
            this.panel1.Controls.Add(this.timeMouseClick_tbx);
            this.panel1.Controls.Add(this.timeRun_tbx);
            this.panel1.Controls.Add(this.mousePos_tbx);
            this.panel1.Controls.Add(this.comboBox);
            this.panel1.Controls.Add(this.text_tbx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nameForm_tbx);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 545);
            this.panel1.TabIndex = 1;
            // 
            // focus_btn
            // 
            this.focus_btn.Location = new System.Drawing.Point(190, 3);
            this.focus_btn.Name = "focus_btn";
            this.focus_btn.Size = new System.Drawing.Size(75, 23);
            this.focus_btn.TabIndex = 7;
            this.focus_btn.Text = "TestFocus";
            this.focus_btn.UseVisualStyleBackColor = true;
            this.focus_btn.Click += new System.EventHandler(this.focus_btn_Click);
            // 
            // isEnable
            // 
            this.isEnable.AutoSize = true;
            this.isEnable.Checked = true;
            this.isEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isEnable.Location = new System.Drawing.Point(115, 5);
            this.isEnable.Name = "isEnable";
            this.isEnable.Size = new System.Drawing.Size(66, 17);
            this.isEnable.TabIndex = 6;
            this.isEnable.Text = "isEnable";
            this.isEnable.UseVisualStyleBackColor = true;
            this.isEnable.CheckedChanged += new System.EventHandler(this.isEnable_CheckedChanged);
            // 
            // form1_cbx
            // 
            this.form1_cbx.AutoSize = true;
            this.form1_cbx.Location = new System.Drawing.Point(114, 106);
            this.form1_cbx.Name = "form1_cbx";
            this.form1_cbx.Size = new System.Drawing.Size(179, 17);
            this.form1_cbx.TabIndex = 6;
            this.form1_cbx.Text = "send back key after mouse click";
            this.form1_cbx.UseVisualStyleBackColor = true;
            // 
            // timeMouseClick_tbx
            // 
            this.timeMouseClick_tbx.Location = new System.Drawing.Point(275, 181);
            this.timeMouseClick_tbx.Name = "timeMouseClick_tbx";
            this.timeMouseClick_tbx.Size = new System.Drawing.Size(67, 20);
            this.timeMouseClick_tbx.TabIndex = 5;
            this.timeMouseClick_tbx.Text = "5";
            // 
            // timeRun_tbx
            // 
            this.timeRun_tbx.Location = new System.Drawing.Point(275, 155);
            this.timeRun_tbx.Name = "timeRun_tbx";
            this.timeRun_tbx.Size = new System.Drawing.Size(67, 20);
            this.timeRun_tbx.TabIndex = 5;
            this.timeRun_tbx.Text = "1";
            // 
            // mousePos_tbx
            // 
            this.mousePos_tbx.Location = new System.Drawing.Point(114, 129);
            this.mousePos_tbx.Name = "mousePos_tbx";
            this.mousePos_tbx.Size = new System.Drawing.Size(237, 20);
            this.mousePos_tbx.TabIndex = 5;
            this.mousePos_tbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mousePos_tbx_KeyDown);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(115, 79);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(237, 21);
            this.comboBox.TabIndex = 4;
            this.comboBox.DropDown += new System.EventHandler(this.refresh_btn_Click);
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // text_tbx
            // 
            this.text_tbx.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.text_tbx.Location = new System.Drawing.Point(0, 222);
            this.text_tbx.Name = "text_tbx";
            this.text_tbx.Size = new System.Drawing.Size(354, 319);
            this.text_tbx.TabIndex = 3;
            this.text_tbx.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "FormTextKey";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(169, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "TimeMouseClick(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(169, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "TimeRun(second)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "FormMousePos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "FormName";
            // 
            // nameForm_tbx
            // 
            this.nameForm_tbx.Location = new System.Drawing.Point(114, 44);
            this.nameForm_tbx.Name = "nameForm_tbx";
            this.nameForm_tbx.Size = new System.Drawing.Size(237, 20);
            this.nameForm_tbx.TabIndex = 0;
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
        private System.Windows.Forms.TextBox timeMouseClick_tbx;
        private System.Windows.Forms.TextBox timeRun_tbx;
        private System.Windows.Forms.TextBox mousePos_tbx;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.RichTextBox text_tbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameForm_tbx;
        private System.Windows.Forms.CheckBox isEnable;
        private System.Windows.Forms.Button focus_btn;
    }
}
