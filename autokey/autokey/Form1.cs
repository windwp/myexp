using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EdgeTool;
using ServiceStack.Text;

namespace autokey
{
    public partial class Form1 : Form
    {
        private AutoFormControl[] _formControls;
        private AutoFormData[] _formDatas;
        private AutoWork _work;
        private Y2KeyboardHook _keyhook;
        public Form1()
        {
            InitializeComponent();
            AbstractWork.LogText = log_rbtx;
            numberForm_tbx.Text = auto.Default.NumberForm.ToString();
            InitPanel();
            InitHook();
            totalTime_tbx.Text = auto.Default.TotalTime;
            key_back_tbx.Text = auto.Default.KeyBackData;
            this.KeyPreview = true;
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
            InitFormData();
            var jsonData = JsonSerializer.SerializeToString(this._formDatas);
            auto.Default.FormJsonData = jsonData;
            auto.Default.NumberForm = int.Parse(numberForm_tbx.Text);
            auto.Default.TotalTime = totalTime_tbx.Text;
            auto.Default.KeyBackData = key_back_tbx.Text;
            auto.Default.Save();
            _keyhook.Uninstall();
        }

        public void InitPanel()
        {
            var numberForm = int.Parse(numberForm_tbx.Text);
            _formControls = new AutoFormControl[numberForm];
            var listItem = new List<AutoFormData>();
            if (auto.Default.FormJsonData.Length > 10)
            {
                listItem = JsonSerializer.DeserializeFromReader<List<AutoFormData>>(new StringReader(auto.Default.FormJsonData));

            }
            this.flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < numberForm; i++)
            {
                var form = new AutoFormControl();
                this._formControls[i] = form;
                if (i < listItem.Count)
                {
                    form.FormData = listItem[i];
                }
                this.flowLayoutPanel1.Controls.Add(form);
            }

        }


        private void start_btn_Click(object sender, EventArgs e)
        {
            InitFormData();
            foreach (var autoFormData in this._formDatas)
            {
                autoFormData.KeyBackToPreviousLocation = string.IsNullOrEmpty(key_back_tbx.Text) ? "%{subtract}" : KeydataConvert.KeyDisplayToSendKey(key_back_tbx.Text);
                if (autoFormData.Title.Length > 4 && autoFormData.Pid == IntPtr.Zero)
                {
                    MessageBox.Show("Form :" + autoFormData.Title + " is not valid");
                    return;
                }
            }
            if (_work != null && _work.IsRun)
            {
                _work.Stop();
            }

            _work = new AutoWork(this._formDatas, Convert.ToInt32(totalTime_tbx.Text));
            _work.Start();

        }

        private void InitFormData()
        {
            this._formDatas = _formControls.Select((item) => item.FormData).ToArray().Where(o => o.IsEnable).ToArray();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            if (_work != null && _work.IsRun)
            {
                _work.Stop();
            }
        }


        private void numberForm_tbx_TextChanged(object sender, EventArgs e)
        {
            InitPanel();
        }

        private void key_back_tbx_KeyDown(object sender, KeyEventArgs e)
        {
            key_back_tbx.Text =KeydataConvert.KeyDisplay(e.KeyData);
            e.Handled = true;
        }
    }
}
