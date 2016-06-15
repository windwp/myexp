using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Label = System.Reflection.Emit.Label;

namespace autokey
{
    public abstract class AbstractWork
    {
        public bool IsRun;
        public string InPutFileSource = "";
        public string OutPutFolder = "./out/";
        public string InPutFolder = "./in/";
        public string InPutFileSoureReplace;
        public static ProgressBar GProgressBar;
        public Label StatuLabel;
        protected int _currentNumWork;
        protected int _currentTotalWork;
        public static RichTextBox LogText;
        public TextReader reader;
        public TextWriter writer;
        public BackgroundWorker OWorker;
        public int StartValue = 0;
        public int FinishValue = 0;
        public int CurrentProcessValue = 0;

        public int PercentProcess
        {
            get { return (int)Math.Round((CurrentProcessValue - StartValue) * 1.0 / (FinishValue - StartValue) * 100); }
        }

        protected bool IsLoop { get; set; }

        protected abstract void DoWork(object sender, DoWorkEventArgs e);
        public void Start()
        {
            if (IsRun) { return; }
            OWorker = new BackgroundWorker();
            OWorker.WorkerReportsProgress = true;
            OWorker.DoWork += DoWork;
            OWorker.ProgressChanged += bw_ProgressChanged;
            OWorker.RunWorkerCompleted += _RunWorkerCompleted;
            OWorker.RunWorkerAsync();
            LogInfo("==========================================");
            LogInfo("START");
            OWorker.WorkerSupportsCancellation = true;
            IsRun = true;
        }

        public void Stop()
        {
            if (!IsRun) return;
            IsLoop = false;
            IsRun = false;
            if (!OWorker.IsBusy)
            {
                OWorker.CancelAsync();
                OWorker.Dispose();
            }
        }

        private void _RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsRun = false;
            bw_RunWorkerCompleted(sender, e);
        }
        protected abstract void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);


        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (LogText != null) LogText.AppendText(e.UserState.ToString());
            if (GProgressBar != null) GProgressBar.Value = PercentProcess;
        }

        protected void LogInfo(string msg)
        {
            if (LogText != null) LogText.Invoke(((MethodInvoker)delegate
                                                                     {
                                                                         LogText.AppendText(msg + "\n");
                                                                     }));
            // log.Info(msg);
        }
    }


}
