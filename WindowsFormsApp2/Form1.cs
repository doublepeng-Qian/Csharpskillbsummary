using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{ 
    public partial class Form1 : Form
    {
        UserHepler helper = new UserHepler("");
        List<User> listUser = new List<User>();
        public Form1()
        {
            InitializeComponent();
            helper.CheckSupperUser("user.pt", listUser);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            SetForm setForm = new SetForm();
            setForm.ShowDialog();
        }
    }
}
