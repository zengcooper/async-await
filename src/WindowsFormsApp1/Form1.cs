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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //异步Lambda 表达式
            //async (sender, e) 异步表达式
            btnDo.Click += async (sender, e) =>
            {
                Do(false, "Doing");

                await Task.Delay(3000);

                Do(true, "Finished");
            };
        }
        /// <summary>
        /// 【发现的问题】
        /// ①好像没有变成“Doing”？
        /// ②并且拖动窗口的时候卡住不动了？
        /// ③3秒后突然变到想拖动到的位置？
        /// ④同时文本变成“Complete”？
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnDo_Click(object sender, EventArgs e)
        //{
        //    btnDo.Enabled = false;
        //    lblText.Text = @"Doing";

        //    Thread.Sleep(3000);

        //    btnDo.Enabled = true;
        //    lblText.Text = @"Complete";
        //}
        #region  async/await
        private async void btnDo_Click(object sender, EventArgs e)
        {
            btnDo.Enabled = false;
            lblText.Text = @"Doing";

            await Task.Delay(3000);

            btnDo.Enabled = true;
            lblText.Text = @"Complete";
        }
        #endregion
        private void Do(bool isEnable, string text)
        {
            btnDo.Enabled = isEnable;
            lblText.Text = text;
        }
    }
}
