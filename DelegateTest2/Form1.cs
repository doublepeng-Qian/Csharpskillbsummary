using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateTest2
{
    //[1]声明委托
    public delegate void ShowCountDelegate(string count);

    public partial class Form1 : Form
    {
        //[3]创建委托对象
        public ShowCountDelegate objShowCountDelegate;

        public Form1()
        {
            InitializeComponent();

            Form2 objfrm1 = new Form2();
            Form3 objfrm2 = new Form3();
            Form4 objfrm3 = new Form4();

            //[4]委托对象关联从窗体方法
            objShowCountDelegate += objfrm1.Receiver;
            objShowCountDelegate += objfrm2.Receiver;
            objShowCountDelegate += objfrm3.Receiver;
            objfrm1.Show();
            objfrm2.Show();
            objfrm3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            count++;
            objShowCountDelegate.Invoke(count.ToString());//[5]利用委托调用方法
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            objShowCountDelegate.Invoke("0");
        }
    }
}
