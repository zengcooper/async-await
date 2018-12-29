using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form3 : Form
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        public Form3()
        {
            InitializeComponent();

            //设置 BackgroundWorker 属性
            _worker.WorkerReportsProgress = true;   //能否报告进度更新
            _worker.WorkerSupportsCancellation = true;  //是否支持异步取消

            //连接 BackgroundWorker 对象的处理程序
            _worker.DoWork += _worker_DoWork;   //开始执行后台操作时触发，即调用 BackgroundWorker.RunWorkerAsync 时触发
            _worker.ProgressChanged += _worker_ProgressChanged; //调用 BackgroundWorker.ReportProgress(System.Int32) 时触发
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;   //当后台操作已完成、被取消或引发异常时触发

        }

        /// <summary>
        /// 当后台操作已完成、被取消或引发异常时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(e.Cancelled ? $@"进程已被取消：{progressBar.Value}%" : $@"进程执行完成：{progressBar.Value}%");
            progressBar.Value = 0;
        }

        /// <summary>
        /// 调用 BackgroundWorker.ReportProgress(System.Int32) 时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;   //异步任务的进度百分比
        }

        /// <summary>
        /// 开始执行后台操作触发，即调用 BackgroundWorker.RunWorkerAsync 时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            if (worker == null)
            {
                return;
            }

            for (var i = 0; i < 10; i++)
            {
                //判断程序是否已请求取消后台操作
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                worker.ReportProgress((i + 1) * 10);    //触发 BackgroundWorker.ProgressChanged 事件
                Thread.Sleep(250);  //线程挂起 250 毫秒
            }
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            //判断 BackgroundWorker 是否正在执行异步操作
            if (!_worker.IsBusy)
            {
                _worker.RunWorkerAsync();   //开始执行后台操作
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _worker.CancelAsync();  //请求取消挂起的后台操作
        }
    }
}
