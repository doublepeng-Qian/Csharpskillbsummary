using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateTest1
{
    public delegate void ShowCountDelegate(string content);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form2 objFrm2 = new Form2();
            objFrm2.msgsender = this.Receiver;
            objFrm2.Show();
        }

        private void Receiver(string content)
        {
            this.label2.Text = content;
        }
    }
}
