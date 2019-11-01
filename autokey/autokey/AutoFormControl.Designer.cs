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
            this.tab_group_box = new System.Windows.Forms.GroupBox();
            this.number_tab_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.is_chrome_cb = new System.Windows.Forms.CheckBox();
            this.is_vscode_cb = new System.Windows.Forms.CheckBox();
            this.tab_run_time_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.is_use_tab_cb = new System.Windows.Forms.CheckBox();
            this.trim_begin_line_cb = new System.Windows.Forms.CheckBox();
            this.focus_btn = new System.Windows.Forms.Button();
            this.isEnable = new System.Windows.Forms.CheckBox();
            this.is_send_back_key_cbx = new System.Windows.Forms.CheckBox();
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.tab_group_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tab_group_box);
            this.panel1.Controls.Add(this.is_use_tab_cb);
            this.panel1.Controls.Add(this.trim_begin_line_cb);
            this.panel1.Controls.Add(this.focus_btn);
            this.panel1.Controls.Add(this.isEnable);
            this.panel1.Controls.Add(this.is_send_back_key_cbx);
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
            // tab_group_box
            // 
            this.tab_group_box.Controls.Add(this.number_tab_txt);
            this.tab_group_box.Controls.Add(this.label3);
            this.tab_group_box.Controls.Add(this.is_chrome_cb);
            this.tab_group_box.Controls.Add(this.is_vscode_cb);
            this.tab_group_box.Controls.Add(this.tab_run_time_txt);
            this.tab_group_box.Controls.Add(this.label4);
            this.tab_group_box.Location = new System.Drawing.Point(6, 215);
            this.tab_group_box.Name = "tab_group_box";
            this.tab_group_box.Size = new System.Drawing.Size(336, 77);
            this.tab_group_box.TabIndex = 9;
            this.tab_group_box.TabStop = false;
            this.tab_group_box.Text = "Tab Setting";
            // 
            // number_tab_txt
            // 
            this.number_tab_txt.Location = new System.Drawing.Point(84, 19);
            this.number_tab_txt.Name = "number_tab_txt";
            this.number_tab_txt.Size = new System.Drawing.Size(67, 20);
            this.number_tab_txt.TabIndex = 5;
            this.number_tab_txt.Text = "2";
            this.number_tab_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "TabNumber";
            // 
            // is_chrome_cb
            // 
            this.is_chrome_cb.AutoSize = true;
            this.is_chrome_cb.BackColor = System.Drawing.Color.Transparent;
            this.is_chrome_cb.Location = new System.Drawing.Point(116, 44);
            this.is_chrome_cb.Name = "is_chrome_cb";
            this.is_chrome_cb.Size = new System.Drawing.Size(69, 17);
            this.is_chrome_cb.TabIndex = 8;
            this.is_chrome_cb.Text = "isChrome";
            this.is_chrome_cb.UseVisualStyleBackColor = false;
            this.is_chrome_cb.CheckedChanged += new System.EventHandler(this.is_chrome_cb_CheckedChanged);
            // 
            // is_vscode_cb
            // 
            this.is_vscode_cb.AutoSize = true;
            this.is_vscode_cb.BackColor = System.Drawing.Color.Transparent;
            this.is_vscode_cb.Location = new System.Drawing.Point(13, 44);
            this.is_vscode_cb.Name = "is_vscode_cb";
            this.is_vscode_cb.Size = new System.Drawing.Size(97, 17);
            this.is_vscode_cb.TabIndex = 8;
            this.is_vscode_cb.Text = "isVsCodeEditor";
            this.is_vscode_cb.UseVisualStyleBackColor = false;
            this.is_vscode_cb.CheckedChanged += new System.EventHandler(this.is_vscode_cb_CheckedChanged);
            // 
            // tab_run_time_txt
            // 
            this.tab_run_time_txt.Location = new System.Drawing.Point(263, 19);
            this.tab_run_time_txt.Name = "tab_run_time_txt";
            this.tab_run_time_txt.Size = new System.Drawing.Size(67, 20);
            this.tab_run_time_txt.TabIndex = 5;
            this.tab_run_time_txt.Text = "15";
            this.tab_run_time_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tab Run Time(s)";
            // 
            // is_use_tab_cb
            // 
            this.is_use_tab_cb.AutoSize = true;
            this.is_use_tab_cb.BackColor = System.Drawing.Color.Transparent;
            this.is_use_tab_cb.Location = new System.Drawing.Point(6, 192);
            this.is_use_tab_cb.Name = "is_use_tab_cb";
            this.is_use_tab_cb.Size = new System.Drawing.Size(71, 17);
            this.is_use_tab_cb.TabIndex = 8;
            this.is_use_tab_cb.Text = "isUseTab";
            this.is_use_tab_cb.UseVisualStyleBackColor = false;
            this.is_use_tab_cb.CheckedChanged += new System.EventHandler(this.is_use_tab_cb_CheckedChanged);
            // 
            // trim_begin_line_cb
            // 
            this.trim_begin_line_cb.AutoSize = true;
            this.trim_begin_line_cb.BackColor = System.Drawing.Color.Transparent;
            this.trim_begin_line_cb.Checked = true;
            this.trim_begin_line_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trim_begin_line_cb.Location = new System.Drawing.Point(6, 169);
            this.trim_begin_line_cb.Name = "trim_begin_line_cb";
            this.trim_begin_line_cb.Size = new System.Drawing.Size(118, 17);
            this.trim_begin_line_cb.TabIndex = 8;
            this.trim_begin_line_cb.Text = "Trim Text begin line";
            this.trim_begin_line_cb.UseVisualStyleBackColor = false;
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
            // is_send_back_key_cbx
            // 
            this.is_send_back_key_cbx.AutoSize = true;
            this.is_send_back_key_cbx.Location = new System.Drawing.Point(83, 192);
            this.is_send_back_key_cbx.Name = "is_send_back_key_cbx";
            this.is_send_back_key_cbx.Size = new System.Drawing.Size(179, 17);
            this.is_send_back_key_cbx.TabIndex = 6;
            this.is_send_back_key_cbx.Text = "send back key after mouse click";
            this.is_send_back_key_cbx.UseVisualStyleBackColor = true;
            // 
            // timeMouseClick_tbx
            // 
            this.timeMouseClick_tbx.Location = new System.Drawing.Point(281, 173);
            this.timeMouseClick_tbx.Name = "timeMouseClick_tbx";
            this.timeMouseClick_tbx.Size = new System.Drawing.Size(67, 20);
            this.timeMouseClick_tbx.TabIndex = 5;
            this.timeMouseClick_tbx.Text = "5";
            this.timeMouseClick_tbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timeRun_tbx
            // 
            this.timeRun_tbx.Location = new System.Drawing.Point(281, 147);
            this.timeRun_tbx.Name = "timeRun_tbx";
            this.timeRun_tbx.Size = new System.Drawing.Size(67, 20);
            this.timeRun_tbx.TabIndex = 5;
            this.timeRun_tbx.Text = "120";
            this.timeRun_tbx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mousePos_tbx
            // 
            this.mousePos_tbx.Location = new System.Drawing.Point(114, 112);
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
            this.text_tbx.Location = new System.Drawing.Point(0, 372);
            this.text_tbx.Name = "text_tbx";
            this.text_tbx.Size = new System.Drawing.Size(354, 169);
            this.text_tbx.TabIndex = 3;
            this.text_tbx.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "FormTextKey";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(175, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "TimeMouseClick(s)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "TimeRun(second)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 112);
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
            this.tab_group_box.ResumeLayout(false);
            this.tab_group_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox is_send_back_key_cbx;
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
        private System.Windows.Forms.CheckBox trim_begin_line_cb;
        private System.Windows.Forms.CheckBox is_vscode_cb;
        private System.Windows.Forms.CheckBox is_chrome_cb;
        private System.Windows.Forms.TextBox number_tab_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox is_use_tab_cb;
        private System.Windows.Forms.GroupBox tab_group_box;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox tab_run_time_txt;
        private System.Windows.Forms.Label label4;
    }
}
