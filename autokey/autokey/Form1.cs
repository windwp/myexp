using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppHook;
using System.Diagnostics;
using System.Threading;
using ManagedWinapi.Windows;

namespace autokey
{
    public partial class Form1 : Form
    {
        AutoFormData form1Data;
        AutoFormData form2Data;
        private AutoWork _work;
        private Y2KeyboardHook _keyhook;
        public Form1()
        {
            InitializeComponent();
            AbstractWork.LogText = log_rbtx;
            doRefresh();
            InitHook();
            textForm1_tbx.Text = auto.Default.form1Key;
            textForm2_tbx.Text = auto.Default.form2Key;
            form1MousePos_tbx.Text = auto.Default.form1Mouse;
            form2MousePos_tbx.Text = auto.Default.form2Mouse;
            form1TimeRun_tbx.Text = auto.Default.form1TimeRun;
            form2TimeRun_tbx.Text = auto.Default.form2TimeRun;
            form1TimeMouseClick_tbx.Text = auto.Default.form1TimeMouseClick;
            form2TimeMouseClick_tbx.Text = auto.Default.form2TimeMouseClick;
            form1TimeWait_tbx.Text = auto.Default.form1TimeWait;
            form2TimeWait_tbx.Text = auto.Default.form2TimeWait;
            form1_cbx.Checked = auto.Default.form1SendKeyBack;
            form2_cbx.Checked = auto.Default.form2SendKeyBack;
            totalTime_tbx.Text = auto.Default.TotalTime;
        }
        private void InitHook()
        {
            //_keyhook = new LowLevelKeyboardHook();
            //_keyhook.MessageIntercepted += _keyhook_MessageIntercepted;
            //_keyhook.StartHook();
            _keyhook = new Y2KeyboardHook();
            _keyhook.Install();
            _keyhook.KeyDown += _keyhook_KeyDown;
        }

        private void _keyhook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    _work?.Stop();
                    break;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            auto.Default.form1Key = textForm1_tbx.Text;
            auto.Default.form2Key = textForm2_tbx.Text;
            auto.Default.form1Mouse = form1MousePos_tbx.Text;
            auto.Default.form2Mouse = form2MousePos_tbx.Text;
            auto.Default.form1TimeRun = form1TimeRun_tbx.Text;
            auto.Default.form2TimeRun = form2TimeRun_tbx.Text;
            auto.Default.form1TimeMouseClick = form1TimeMouseClick_tbx.Text;
            auto.Default.form2TimeMouseClick = form2TimeMouseClick_tbx.Text;
            auto.Default.form1TimeWait = form1TimeWait_tbx.Text;
            auto.Default.form2TimeWait = form2TimeWait_tbx.Text;
            auto.Default.form1SendKeyBack = form1_cbx.Checked;
            auto.Default.form2SendKeyBack = form2_cbx.Checked;
            auto.Default.TotalTime = totalTime_tbx.Text;
            auto.Default.Save();
            _keyhook.Uninstall();
        }

        public void doRefresh()
        {
            form1Data = null;
            form2Data = null;
            Process[] processes = Process.GetProcesses();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (Process process in processes)
            {
                //Get whatever attribute for process
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    comboBox1.DisplayMember = "MainWindowTitle";
                    comboBox2.DisplayMember = "MainWindowTitle";
                    comboBox1.Items.Add(process);
                    comboBox2.Items.Add(process);
                }

            }

        }


        private void start_btn_Click(object sender, EventArgs e)
        {
            Init();
            if (_work != null && _work.IsRun)
            {
                _work.Stop();
            }
            var arr = new AutoFormData[2] { form1Data, form2Data };
            _work = new AutoWork(arr, Convert.ToInt32(totalTime_tbx.Text));
            _work.Start();

        }

        private void Init()
        {
            form1Data = new AutoFormData();
            form1Data.Title = nameForm1_tbx.Text;
            form1Data.TextKeyboard = textForm1_tbx.Text.Trim();
            form1Data.MouseData = form1MousePos_tbx.Text.Trim();
            var process = comboBox1.SelectedItem as Process;
            if (process != null)
            {
                form1Data.Pid = process.MainWindowHandle;
                form1Data.TimeRun = Convert.ToInt32(form1TimeRun_tbx.Text);
                form1Data.TimeMouseClick = Convert.ToInt32(form1TimeMouseClick_tbx.Text);
                form1Data.TimeWait = Convert.ToInt32(form1TimeWait_tbx.Text);
                form1Data.IsSendBackKey = form1_cbx.Checked;
            }
            //if (form1Data.Pid == IntPtr.Zero)
            //{
            //    MessageBox.Show("Form 1 not valid");
            //}
            form2Data = new AutoFormData();
            form2Data.Title = nameForm2_tbx.Text;
            form2Data.TextKeyboard = textForm2_tbx.Text;
            form2Data.MouseData = form2MousePos_tbx.Text;
            process = comboBox2.SelectedItem as Process;
            if (process != null)
            {
                form2Data.Pid = process.MainWindowHandle;
                form2Data.TimeRun = Convert.ToInt32(form2TimeRun_tbx.Text);
                form2Data.TimeMouseClick = Convert.ToInt32(form2TimeMouseClick_tbx.Text);
                form2Data.TimeWait = Convert.ToInt32(form2TimeWait_tbx.Text);
                form2Data.IsSendBackKey = form2_cbx.Checked;
            }
            //if (form2Data.Pid == IntPtr.Zero)
            //{
            //    MessageBox.Show("Form 2 not valid");
            //    return;
            //}
        }

        private void TestFocus1_btn_Click(object sender, EventArgs e)
        {
            Init();
            MWin.SetForegroundWindow(form1Data.Pid);
            MWin.ShowWindow(form1Data.Pid);
        }

        private void TestFocus2_btn_Click(object sender, EventArgs e)
        {
            Init();
            MWin.ShowWindow(form2Data.Pid);
        }

        private void TestSendKey_btn_Click(object sender, EventArgs e)
        {
            if (form1Data == null)
            {
                Init();
            }
            MWin.SetForegroundWindow(form1Data.Pid);
            for (int i = 0; i < 200; i++)
            {
                form1Data.SendKeyBoard();
                Thread.Sleep(100);
            }


        }

        private void TestMouseMove_btn_Click(object sender, EventArgs e)
        {
            Init();
            MWin.ShowWindow(form1Data.Pid);
            form1Data.MoveMouse();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameForm1_tbx.Text = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameForm2_tbx.Text = comboBox2.Text;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            POINT pos;
            if (e.KeyCode == Keys.F10)
            {
                MWin.GetCursorPos(out pos);
                form1MousePos_tbx.Text += $"{pos.X},{pos.Y},";
            }
            else if (e.KeyCode == Keys.F11)
            {
                MWin.GetCursorPos(out pos);
                form2MousePos_tbx.Text += $"{pos.X},{pos.Y},";
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            if (_work != null && _work.IsRun)
            {
                _work.Stop();
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            doRefresh();
            log_rbtx.Text += AutoWork.UnixTimeNow() + "\r\n";
        }
    }
}
