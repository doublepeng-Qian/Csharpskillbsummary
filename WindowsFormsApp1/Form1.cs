using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            /* 不允许同时选定多个文件 */
            fileDialog.Multiselect = false;

            /* 选择要加载的文件格式 */
            fileDialog.Filter = "所有文件(*.*)|*.txt*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                /* 获取加载文件的路径 */
                textBox1.Text = fileDialog.FileName;
                
                StreamReader srReadFile = new StreamReader(fileDialog.FileName);

                // 读取流直至文件末尾结束
                while (!srReadFile.EndOfStream)
                {
                    string strReadLine = srReadFile.ReadLine();
                                                                
                    //
                }

                // 关闭读取流文件
                srReadFile.Close();
                Console.Read();
            }
        }
    }
}
