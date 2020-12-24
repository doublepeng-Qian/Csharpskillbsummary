using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBoxConfigTool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_inputkey.Text != "IVC778899")
            {
                //Form1.GetInterface().IncludeTextMessage("权限限制");
                MessageBox.Show("密码错误，您无有效权限修改参数！");
            }
            else
            {
                MessageBox.Show("身份认证通过");
                //TransferStation.ProcessMsg = "OK";
                Close();//关闭当前的密钥输入窗口

                //显示串口参数配置弹窗
                //UARTconfig uartconfig = new UARTconfig();
                //uartconfig.ShowDialog();
            }
        }
    }
}
