using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppHook;
using ManagedWinapi.Windows;

namespace autokey
{
    public partial class AutoFormControl : UserControl
    {
        public AutoFormControl()
        {
            InitializeComponent();
        }
        public void doRefresh()
        {
            Process[] processes = Process.GetProcesses();
            comboBox.Items.Clear();
            var selectedItem = comboBox.SelectedItem;
            foreach (Process process in processes)
            {
                //Get whatever attribute for process
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    comboBox.DisplayMember = "MainWindowTitle";
                    comboBox.Items.Add(process);
                    if (selectedItem != null && ((Process)selectedItem).MainWindowHandle == process.MainWindowHandle)
                    {
                        comboBox.SelectedItem = process;
                    }
                }

            }

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameForm_tbx.Text = comboBox.Text;
        }
        public AutoFormData FormData
        {
            get
            {
                var value = new AutoFormData();
                value.IsEnable = isEnable.Checked;
                value.Title = nameForm_tbx.Text;
                value.TextKeyboard = text_tbx.Text;
                value.MouseData = mousePos_tbx.Text;
                value.TimeRun = Convert.ToInt32(timeRun_tbx.Text);
                value.TimeMouseClick = Convert.ToInt32(timeMouseClick_tbx.Text);
                value.IsSendBackKey = form1_cbx.Checked;
                var process = comboBox.SelectedItem as Process;
                if (process != null)
                {
                    value.Pid = process.MainWindowHandle;
                }
                return value;

            }
            set
            {
                if (value == null) return;
                nameForm_tbx.Text = value.Title;
                text_tbx.Text = value.TextKeyboard;
                mousePos_tbx.Text = value.MouseData;
                timeRun_tbx.Text = value.TimeRun.ToString();
                timeMouseClick_tbx.Text = value.TimeMouseClick.ToString();
                form1_cbx.Checked = value.IsSendBackKey;
                isEnable.Checked = value.IsEnable;
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            doRefresh();
        }

        private void isEnable_CheckedChanged(object sender, EventArgs e)
        {
            nameForm_tbx.Enabled = isEnable.Checked;
            text_tbx.Enabled = isEnable.Checked;
            mousePos_tbx.Enabled = isEnable.Checked;
            timeRun_tbx.Enabled = isEnable.Checked;
            timeMouseClick_tbx.Enabled = isEnable.Checked;
            form1_cbx.Enabled = isEnable.Checked;
            comboBox.Enabled = isEnable.Checked;
        }

        private void mousePos_tbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                POINT pos;
                MWin.GetCursorPos(out pos);
                mousePos_tbx.Text += $"{pos.X},{pos.Y},";
            }
        }

        private void focus_btn_Click(object sender, EventArgs e)
        {
            var process = this.comboBox.SelectedItem as Process;
            if (process != null) MWin.ShowWindow(process.MainWindowHandle);
        }

        private void TestSendKey_btn_Click(object sender, EventArgs e)
        {
            var process = this.comboBox.SelectedItem as Process;
            if (process != null)
            {
                MWin.SetForegroundWindow(process.MainWindowHandle);
                var formData = this.FormData;
                for (int i = 0; i < 200; i++)
                {
                    formData.SendKeyBoard();
                    Thread.Sleep(100);
                }
            }
            
        }

        private void TestMouseMove_btn_Click(object sender, EventArgs e)
        {
            var process = this.comboBox.SelectedItem as Process;
            if (process != null) MWin.ShowWindow(process.MainWindowHandle);
            this.FormData.MoveMouse();

        }
    }
}
