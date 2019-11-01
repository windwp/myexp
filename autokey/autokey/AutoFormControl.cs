using AppHook;
using ManagedWinapi.Windows;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace autokey
{
    public partial class AutoFormControl : UserControl
    {
        public AutoFormControl()
        {
            InitializeComponent();
            is_use_tab_cb.Checked = false;
            tab_group_box.Visible = is_use_tab_cb.Checked;
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
                var autoFormSetting = new AutoFormData();
                autoFormSetting.IsEnable = isEnable.Checked;
                autoFormSetting.Title = nameForm_tbx.Text;
                autoFormSetting.TextKeyboard = text_tbx.Text;
                autoFormSetting.MouseData = mousePos_tbx.Text;
                autoFormSetting.TimeRun = Convert.ToInt32(timeRun_tbx.Text);
                autoFormSetting.TimeMouseClick = Convert.ToInt32(timeMouseClick_tbx.Text);
                autoFormSetting.IsSendBackKey = is_send_back_key_cbx.Checked;
                autoFormSetting.TrimBeginLine = trim_begin_line_cb.Checked;
                autoFormSetting.IsTab = is_use_tab_cb.Checked;
                autoFormSetting.IsVsCode = is_vscode_cb.Checked;
                autoFormSetting.IsChrome = is_chrome_cb.Checked;
                autoFormSetting.TabRunTime = Convert.ToInt32(tab_run_time_txt.Text);
                autoFormSetting.NumberTab = Convert.ToInt32(number_tab_txt.Text);
                if (autoFormSetting.TrimBeginLine)
                {
                    autoFormSetting.TextKeyboard = Regex.Replace(text_tbx.Text, "^ *", "", RegexOptions.Multiline);
                }
                var process = comboBox.SelectedItem as Process;
                if (process != null)
                {
                    autoFormSetting.Pid = process.MainWindowHandle;
                }
                return autoFormSetting;

            }
            set
            {
                if (value == null) return;
                isEnable.Checked = value.IsEnable;
                nameForm_tbx.Text = value.Title;
                text_tbx.Text = value.TextKeyboard;
                mousePos_tbx.Text = value.MouseData;
                timeRun_tbx.Text = value.TimeRun.ToString();
                timeMouseClick_tbx.Text = value.TimeMouseClick.ToString();
                is_send_back_key_cbx.Checked = value.IsSendBackKey;
                isEnable.Checked = value.IsEnable;
                is_vscode_cb.Checked = value.IsVsCode;
                is_chrome_cb.Checked = value.IsChrome;
                number_tab_txt.Text = value.NumberTab.ToString();
                tab_run_time_txt.Text = value.TabRunTime.ToString();
                is_use_tab_cb.Checked = value.IsTab;
                trim_begin_line_cb.Checked = value.TrimBeginLine;
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
            is_send_back_key_cbx.Enabled = isEnable.Checked;
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

        private void is_vscode_cb_CheckedChanged(object sender, EventArgs e)
        {

            if (is_vscode_cb.Checked)
            {
                is_chrome_cb.Checked = false;
                is_send_back_key_cbx.Checked = true;
            }
        }

        private void is_chrome_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (is_chrome_cb.Checked)
            {
                is_vscode_cb.Checked = false;
                is_send_back_key_cbx.Checked = false;
            }
        }

        private void is_use_tab_cb_CheckedChanged(object sender, EventArgs e)
        {
            tab_group_box.Visible = is_use_tab_cb.Checked;
        }
    }
}
