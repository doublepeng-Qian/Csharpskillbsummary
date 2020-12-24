using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Ribbon;

// using MetroFramework.Forms;

namespace WindowsFormsApp2
{ 
    public partial class Form1 : KryptonForm
    {
        UserHepler helper = new UserHepler("");
        List<User> listUser = new List<User>();
        public Form1()
        {
            InitializeComponent();
            helper.CheckSupperUser("user.pt", listUser);
        }

        /// <summary>
        /// 登录按钮触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 设置按钮触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            SetForm setForm = new SetForm();
            setForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            //kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewDocument(), NewDocument() });
            //kryptonDockingManager.AddDockspace("Control", DockingEdge.Right, new KryptonPage[] { NewPropertyGrid(), NewInput(), NewPropertyGrid(), NewInput() });
            //kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewInput(), NewPropertyGrid(), NewInput(), NewPropertyGrid() });
        }
    }
}
