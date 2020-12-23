﻿using System;
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
    public partial class Form2 : Form
    {

        public ShowCountDelegate msgsender;
        private int content = 0;


        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            content++;
            if (msgsender != null)
            {
                msgsender(content.ToString());//[5]调用
            }
        }
    }
}
