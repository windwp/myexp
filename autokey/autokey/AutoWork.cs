using AppHook;
using ManagedWinapi.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace autokey
{
    internal class AutoWork : AbstractWork
    {
        private readonly AutoFormData[] _formDatas;
        private readonly int _totalSeconds;
        private long _startTime;
        private Random _random;

        public AutoWork(AutoFormData[] formDatas, int totalMiuntes)
        {
            _formDatas = formDatas;
            _totalSeconds = totalMiuntes * 60;
            _random = new Random();
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            IsLoop = true;
            _startTime = UnixTimeNow();
            var index = 0;
            while (IsLoop)
            {
                if (index >= _formDatas.Length) index = 0;
                var formData = _formDatas[index];
                if (formData.IsRun)
                {
                    //stop form and move to next form
                    if (formData.EndTime < UnixTimeNow())
                    {
                        formData.IsRun = false;
                        index += 1;
                        if (index >= _formDatas.Length) index = 0;
                        formData.StartTime = UnixTimeNow() + _formDatas[index].TimeRun;
                        LogInfo("End Form" + formData.Title);
                        Thread.Sleep(3000);
                    }
                    //do form work
                    try
                    {
                        if (formData.IsTab)
                        {
                            if (formData.NextTabTime < UnixTimeNow())
                            {
                                //if (formData.NextTabTime == 0||formData.CurrentTabIndex >= formData.NumberTab) { 
                                //    formData.CurrentTabIndex = -1; 
                                //}
                                //formData.CurrentTabIndex += 1;
                                formData.CurrentTabIndex = _random.Next(0, formData.NumberTab);
                                formData.NextTabTime = UnixTimeNow() + formData.TabRunTime;
                                LogInfo("Switch Tab : " + formData.CurrentTabIndex);
                                if (formData.IsChrome)
                                {
                                    SendKeys.SendWait("^" + (formData.CurrentTabIndex + 1));
                                }
                                if (formData.IsVsCode)
                                {
                                    SendKeys.SendWait("%" + (formData.CurrentTabIndex + 1));
                                }
                                if (formData.IsVsCode && formData.CurrentTab != null && !formData.CurrentTab.IsInit)
                                {
                                    //send control C;
                                    Thread.Sleep(100);
                                    SendKeys.SendWait("^{a}");
                                    Thread.Sleep(100);
                                    SendKeys.SendWait("^{c}");
                                    Thread.Sleep(100);
                                    formData.CurrentTab.Text = Clipboard.GetText(TextDataFormat.Text);
                                    Thread staThread = new Thread(
                                     delegate ()
                                    {
                                        try
                                        {
                                            formData.CurrentTab.Text = Clipboard.GetText(TextDataFormat.Text);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    });
                                    staThread.SetApartmentState(ApartmentState.STA);
                                    staThread.Start();
                                    staThread.Join();
                                    SendKeys.SendWait("{DEL}");
                                    formData.CurrentTab.IsInit = true;
                                }
                                Thread.Sleep(100);
                            }
                        }
                        if (formData.LastMouseClick < UnixTimeNow())
                        {
                            formData.LastMouseClick = UnixTimeNow() + formData.TimeMouseClick;
                            formData.MoveMouse();
                            Thread.Sleep(500);
                        }
                        else
                        {
                            formData.SendKeyBoard();
                            Thread.Sleep(200);
                        }
                    }
                    catch (Exception exception)
                    {
                        LogInfo(formData.CurrentIndexOfCharacter.ToString());
                        LogInfo(exception.Message);
                    }
                }
                else
                {
                    if (formData.StartTime < UnixTimeNow())
                    {
                        formData.StartTime = UnixTimeNow();
                        formData.EndTime = UnixTimeNow() + formData.TimeRun;
                        formData.IsRun = true;
                        MWin.ShowWindow(formData.Pid);
                        LogInfo("Run Form" + formData.Title);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        index += 1;
                    }
                }
                if (_startTime + _totalSeconds < UnixTimeNow())
                {
                    IsLoop = false;
                }
            }
        }

        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        protected override void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }

    public class AutoFormData
    {
        public AutoFormData()
        {
            _listPos = new List<POINT>();
            CurrentTabIndex = 0;
        }

        public IntPtr Pid { get; set; }
        public string Title { get; set; }
        public string TextKeyboard { get; set; }
        public string KeyBackToPreviousLocation { get; set; }
        public bool TrimBeginLine { get; set; }
        public bool IsVsCode { get; set; }
        public bool IsTab { get; set; }
        public bool IsChrome { get; set; }
        public int CurrentTabIndex { get; set; }
        public AutoTabData CurrentTab
        {
            get
            {
                if (this.CurrentTabIndex < 0) this.CurrentTabIndex = 0;
                if (this.CurrentTabIndex > this._listTabDatas.Count) this.CurrentTabIndex = this._listTabDatas.Count;
                return this._listTabDatas[this.CurrentTabIndex];
            }
        }
        public long NextTabTime { get; set; }
        public long TabRunTime { get; set; }
        public int NumberTab
        {
            get => _numberTab;
            set
            {
                _numberTab = value;
                this._listTabDatas = new List<AutoTabData>(_numberTab);

                for (int i = 0; i < _numberTab; i++)
                {
                    var tab = new AutoTabData();
                    this._listTabDatas.Add(tab);

                }
            }
        }
        public string MouseData
        {
            get { return _mouseData; }
            set
            {

                this._mouseData = value;
                if (string.IsNullOrEmpty(value)) return;
                var mousePosData = value.Split(',');
                _listPos.Clear();
                for (int i = 0; i < mousePosData.Length - 1; i += 2)
                {
                    _listPos.Add(new POINT(Convert.ToInt32(mousePosData[i]), Convert.ToInt32(mousePosData[i + 1])));
                }
                if (_listPos.Count > 0)
                {
                    IsMouseMove = true;
                }
            }
        }

        public bool IsMouseMove { get; set; }
        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public long LastMouseClick { get; set; }

        public long TimeRun { get; set; }
        public long TimeMouseClick { get; set; }
        //        public long TimeWait { get; set; }
        public bool IsRun { get; set; }
        public bool IsEnable { get; set; }
        public bool IsSendBackKey { get; set; }

        public int CurrentIndexOfCharacter
        {
            get { return _currentIndexOfCharacter; }
        }

        private int _currentIndexOfCharacter;
        private int _currentIndexOfMouse;
        private string _mouseData;
        private List<POINT> _listPos;
        private int _numberTab;
        private List<AutoTabData> _listTabDatas;

        public void MoveMouse()
        {
            if (_currentIndexOfMouse >= _listPos.Count)
            {
                _currentIndexOfMouse = 0;
            }
            if (_currentIndexOfMouse < _listPos.Count && _listPos.Count > 0)
            {
                MWin.ClickPosition(Pid, _listPos[_currentIndexOfMouse]);
                _currentIndexOfMouse += 1;
                if (IsSendBackKey)
                {
                    Thread.Sleep(600);
                    //https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys(v=vs.110).aspx
                    SendKeys.SendWait(KeyBackToPreviousLocation);
                }
            }
            else
            {
                //                SendKeyBoard();
            }
        }

        public void SendKeyBoard()
        {
            if (this.IsTab && this.IsVsCode)
            {
                var tab = this.CurrentTab;

                if (tab.TextIndex > tab.Text.Length)
                {
                    tab.TextIndex = 0;
                }
                if (tab.TextIndex < tab.Text.Length && tab.Text.Length > 0)
                {
                    var character = tab.Text.Substring(tab.TextIndex, 1);
                    tab.TextIndex += 1;
                    string txt = Regex.Replace(character, @"[+^%~(){}]", "{$0}");
                    SendKeys.SendWait(txt);
                }
                return;
            }
            if (_currentIndexOfCharacter >= TextKeyboard.Length)
            {
                _currentIndexOfCharacter = 0;
            }
            if (_currentIndexOfMouse < TextKeyboard.Length && TextKeyboard.Length > 0)
            {
                var character = TextKeyboard.Substring(_currentIndexOfCharacter, 1);
                _currentIndexOfCharacter += 1;
                string txt = Regex.Replace(character, @"[+^%~(){}]", "{$0}");
                SendKeys.SendWait(txt);
            }
            else
            {
                MoveMouse();
            }
        }
    }
}
public class AutoTabData
{
    public string Text { get; set; }
    public int TextIndex { get; set; }

    public bool IsInit { get; set; }
    public AutoTabData()
    {
        Text = "";
        TextIndex = 0;
        IsInit = false;

    }
}