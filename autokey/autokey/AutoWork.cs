using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppHook;
using ManagedWinapi.Windows;

namespace autokey
{
    internal class AutoWork : AbstractWork
    {
        private readonly AutoFormData[] _formList;
        private readonly int _totalSeconds;
        private long _startTime;
        private Random _random;

        public AutoWork(AutoFormData[] formList, int totalMiuntes)
        {
            _formList = formList;
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
                if (index >= _formList.Length) index = 0;
                var formData = _formList[index];
                if (formData.IsRun)
                {
                    //stop form and move to next form
                    if (formData.EndTime < UnixTimeNow())
                    {
                        formData.IsRun = false;
                        formData.StartTime = UnixTimeNow() + formData.TimeWait;
                        LogInfo("End Form" + formData.Title);
                        index += 1;
                        Thread.Sleep(3000);
                    }
                    //do form work
                    try
                    {
                        if (formData.LastMouseClick  < UnixTimeNow())
                        {
                            formData.LastMouseClick = UnixTimeNow()+formData.TimeMouseClick;
                            formData.MoveMouse();
                            Thread.Sleep(1000);
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
                        LogInfo(" Run Form" + formData.Title);
                        Thread.Sleep(3000);
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

    internal class AutoFormData
    {
        public AutoFormData()
        {
            _listPos = new List<POINT>();
        }

        public IntPtr Pid { get; set; }
        public string Title { get; set; }
        public string TextKeyboard { get; set; }

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
        public long TimeWait { get; set; }
        public bool IsRun { get; set; }
        public bool IsSendBackKey { get; set; }

        public int CurrentIndexOfCharacter
        {
            get { return _currentIndexOfCharacter; }
        }

        private int _currentIndexOfCharacter;
        private int _currentIndexOfMouse;
        private string _mouseData;
        private List<POINT> _listPos;


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
                    Thread.Sleep(100);
                    SendKeys.SendWait("%{SUBTRACT}"); //back to location in sublime
                }
            }
            else
            {
                //                SendKeyBoard();
            }
        }

        public void SendKeyBoard()
        {
            if (_currentIndexOfCharacter >= TextKeyboard.Length)
            {
                _currentIndexOfCharacter = 0;
            }
            if (_currentIndexOfMouse < TextKeyboard.Length && TextKeyboard.Length > 0)
            {
                var character = TextKeyboard.Substring(_currentIndexOfCharacter, 1);
                //            MWin.KeyDownAndUp(Pid, VirtualKeyStates.VK_OEM_COMMA);
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