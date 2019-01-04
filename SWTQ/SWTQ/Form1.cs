using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWTQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate("https://www.meiriyiwen.com");
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
        }

        void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            foreach (HtmlElement ele in webBrowser.Document.GetElementsByTagName("div"))
            {
                var className = ele.GetAttribute("className");
                if (className == "article_text")
                {
                    txtContent.Text = ele.InnerText;
                }
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            lblCount.Text = $"总字数：{txtContent.Text.Replace("\r", "").Replace("\n", "").Length}";
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("https://meiriyiwen.com/random");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            txtContent.Text = txtContent.Text.Replace("\r", "").Replace("\n", "").Replace(" ","");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            //设置保存文件对话框的标题
            sfd.Title = "请选择要保存的文件路径";
            //初始化保存目录，默认桌面
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //设置保存文件的类型
            sfd.Filter = "文本文件|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //获得保存文件的路径
                string filePath = sfd.FileName;
                //保存
                using (var ws = new StreamWriter(filePath, false))
                {
                    ws.Write(txtContent.Text);
                }
                MessageBox.Show("保存成功！");
            }
        }
    }
}
