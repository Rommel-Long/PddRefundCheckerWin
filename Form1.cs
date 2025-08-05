using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PddRefundCheckerWin
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> _results = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("请先选择有效的 Excel 文件");
                return;
            }

            btnStart.Enabled = false;
            logBox.AppendText("开始处理...");

            var checker = new AlibabaChecker();
            checker.EnsureLogin();

            var trackingNumbers = ExcelHelper.ReadTrackingNumbers(filePath);
            _results.Clear();

            for (int i = 0; i < trackingNumbers.Count; i++)
            {
                var tn = trackingNumbers[i];
                var status = checker.CheckStatus(tn);
                _results[tn] = status;
                logBox.AppendText($"[{i + 1}/{trackingNumbers.Count}] {tn} -> {status}");
                progressBar.Value = (int)((i + 1) * 100.0 / trackingNumbers.Count);
                Thread.Sleep(new Random().Next(2000, 4000));
            }

            checker.Quit();
            ExcelHelper.WriteStatus(filePath, _results);
            logBox.AppendText("处理完成，结果保存在 output.xlsx");
            btnStart.Enabled = true;
        }
    }
}