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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private CancellationTokenSource _source;
        private CancellationToken _token;
        /// <summary>
        /// Do 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDo_Click(object sender, EventArgs e)
        {
            btnDo.Enabled = false;

            _source = new CancellationTokenSource();
            _token = _source.Token;

            var completedPercent = 0; //完成百分比
            const int time = 10; //循环次数
            const int timePercent = 100 / time; //进度条每次增加的进度值

            for (var i = 0; i < time; i++)
            {
                if (_token.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    await Task.Delay(500, _token);
                    completedPercent = (i + 1) * timePercent;
                }
                catch (Exception)
                {
                    completedPercent = i * timePercent;
                }
                finally
                {
                    progressBar.Value = completedPercent;
                }
            }

            var msg = _token.IsCancellationRequested ? $"进度为：{completedPercent}% 已被取消！" : $"已经完成";

            MessageBox.Show(msg, @"信息");

            progressBar.Value = 0;
            InitTool();
        }
        /// <summary>
        /// 初始化窗体的工具控件
        /// </summary>
        private void InitTool()
        {
            progressBar.Value = 0;
            btnDo.Enabled = true;
            btnCancel.Enabled = true;
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnDo.Enabled) return;

            btnCancel.Enabled = false;
            _source.Cancel();
        }
    }
}
