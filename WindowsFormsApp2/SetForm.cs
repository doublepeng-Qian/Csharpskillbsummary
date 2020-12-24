using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Ribbon;
//using MetroFramework.Forms;

namespace WindowsFormsApp2
{
    public partial class SetForm : KryptonForm
    {
        UserHepler helper = new UserHepler("");
        List<User> listUser = new List<User>();
        public SetForm()
        {
            InitializeComponent();

            if (File.Exists("user.pt"))
            {
                listUser = helper.DeSerializedUser("user.pt");
                ShowUserList(listUser);
            }
        }

        public void ShowUserList(List<User> list)
        {
            List<User> Uplist = new List<User>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    Uplist.Add(list[i]);
                }
            }
            dgvManger.DataSource = null;
            dgvManger.DataSource = Uplist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private User GetUser(List<User> list)
        {
            if (txtUser.Text == string.Empty | txtPassWord.Text == string.Empty)
            {
                return null;
            }
            User user = new User();
            user.Index = list[list.Count - 1].Index + 1;
            user.UserName = txtUser.Text;
            user.PassWord = txtPassWord.Text;
            switch (cmbLevel.SelectedIndex)
            {
                case 0:
                    user.Level = Authority.Admin;
                    break;
                case 1:
                    user.Level = Authority.Eng;
                    break;
                case 2:
                    user.Level = Authority.Op;
                    break;
                default:
                    break;
            }
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetUser()
        {
            DataGridViewSelectedRowCollection rowCollection = dgvManger.SelectedRows;
            if (rowCollection.Count == 0) return;
            DataGridViewRow row = rowCollection[0];
            txtUser.Text = row.Cells[1].Value.ToString();
            txtPassWord.Text = row.Cells[2].Value.ToString();
            switch (row.Cells[3].Value.ToString())
            {
                case "Admin":
                    cmbLevel.SelectedIndex = 0;
                    break;
                case "Eng":
                    cmbLevel.SelectedIndex = 1;
                    break;
                case "Op":
                    cmbLevel.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = GetUser(listUser);

            if (user == null)
            {
                MessageBox.Show("输入内容错误！");
            }

            var res = helper.AddUser("user.pt", listUser, user);

            if (res)
            {
                ShowUserList(listUser);
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }

        private void dgvManger_SelectionChanged(object sender, EventArgs e)
        {
            SetUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == string.Empty)
            {
                MessageBox.Show("删除用户名未输入！");
                return;
            }
            var res = helper.DeleteUser("user.pt", listUser, txtUser.Text);

            if (res)
            {
                ShowUserList(listUser);
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            User user = GetUser(listUser);
            var res = helper.EditUser("user.pt", listUser, user);

            if (res)
            {
                ShowUserList(listUser);
                MessageBox.Show("修改成功！");
            }
            else
            {
                MessageBox.Show("修改失败！");
            }
        }
    }
}
